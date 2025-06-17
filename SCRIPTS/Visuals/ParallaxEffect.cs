using System.Collections; // Grants access to collections and data structures like ArrayList, Hashtable, etc.
using System.Collections.Generic; // grants access to collections and data structures like List, Dictionary, etc.
using UnityEngine; // Grants access to Unity's core classes and functions, such as GameObject, MonoBehaviour, etc.

public class ParallaxEffect : MonoBehaviour
{
    private float initialPositionX, length; // stores the start position of the object and the length of the object
    public GameObject characterCam; // reference to the camera object
    public float parallaxSpeed; // speed at which the background move relative to the camera

    void Start() // Start is called before the first frame update
    {
        initialPositionX = transform.position.x; // get the initial position of the object
        length = GetComponent<SpriteRenderer>().bounds.size.x; // get the length of the object
    }

    void FixedUpdate() // Fixed Update is called once per evenly distributed frame
    {
        float distance = characterCam.transform.position.x * parallaxSpeed; // calculate the distance moved by the camera (keep it near 0.5 to achieve parallax)
        float movement = characterCam.transform.position.x * (1 - parallaxSpeed); // calculates the offset between the camera and background to check when looping is needed

        transform.position = new Vector3(initialPositionX + distance, transform.position.y, transform.position.z); // stores the new position of the camera (stores displacement)

        if (movement > initialPositionX + length) // if the camera has moved past the object
        {
            initialPositionX += length; // reset the initial position to the new position
        }
        else if (movement < initialPositionX - length) // if the camera has moved past the object
        {
            initialPositionX -= length; // reset the initial position to the new position
        }
    }
}
