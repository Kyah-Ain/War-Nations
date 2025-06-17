using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 
using System; // Grants access to the System namespace for using Action delegates

public class Bird : MonoBehaviour
{
    public event Action<Bird> OnBirdStopped; // Event for launcher
    public GameObject launchSFX; // Reference to the launch sound effect prefab

    protected Rigidbody2D _rb; // Reference to the Rigidbody2D component of the bird
    public float birdMass = 15f; // Set the mass of the bird in the inspector
    public float birdDrag = 4.5f; // Set the drag of the bird in the inspector

    protected bool _launched = false; // Flag to check if the bird has been launched
    protected float _stopThreshold = 0.05f; // Threshold for stopping the bird
    protected float _checkTime = 2f; // Time to check if the bird has stopped moving
    protected float _timer = 0f; // Timer to track how long the bird has been stopped

    void Awake() // Awake is called when the script instance is being loaded
    {
        _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the bird
    }

    public virtual void Launch(Vector2 force) // Method to launch the bird with a specified force
    {
        _launched = true; // Set the launched flag to true to indicate the bird has been launched
        _rb.isKinematic = false; // Set the Rigidbody2D to non-kinematic to allow physics interactions
        _rb.AddForce(force, ForceMode2D.Impulse); // Apply the force to the bird's Rigidbody2D as an impulse

        _rb.mass = birdMass; // Set the universal weight of the bird
        _rb.angularDrag = birdDrag; // Set the angular drag to slow down the rotation

        // Launched SFX
        if (launchSFX != null)
        {
            Instantiate(launchSFX, transform.position, Quaternion.identity); // Instantiate the launch sound effect at the bird's position
        }
    }

    protected void Update()
    {
        if (!_launched) return; // If the bird has not been launched, do nothing

        if (_rb.velocity.magnitude < _stopThreshold) // Check if the bird's velocity is below the stop threshold
        {
            _timer += Time.deltaTime; // Increment the timer by the time since the last frame

            if (_timer > _checkTime) // If the timer exceeds the check time
            {
                OnBirdStopped?.Invoke(this); // Trigger the OnBirdStopped event to notify the launcher
                enabled = false; // Disable this script to stop further updates
            }
        }
        else
        {
            _timer = 0f; // Reset timer if bird moves
        }
    }

    protected void TriggerBirdStopped() // Method to call when the bird stops moving
    {
        OnBirdStopped?.Invoke(this); // Invoke the OnBirdStopped event to notify any listeners
    }
}
