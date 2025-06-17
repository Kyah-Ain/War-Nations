using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 

public class RussianBombBird : Bird
{
    public GameObject explosionEffect; // Assign in Inspector
    public float explosionRadius = 5f; // Radius of the explosion effect
    public float explosionForce = 700f; // Force applied to objects in the explosion radius
    private bool hasExploded = false; // Flag to check if the bird has already exploded

    void Update()
    {
        base.Update(); // Keep the stopped-check logic

        if (_launched && !hasExploded && Input.GetMouseButtonDown(0)) // Check for mouse click to trigger explosion
        {
            Explode(); // Call the Explode method when the mouse button is pressed
        }
    }

    private void Explode() // Method to handle the explosion logic
    {
        hasExploded = true; // Set the flag to true to prevent multiple explosions

        // Optional: Explosion visual effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity); // Instantiate the explosion effect at the bird's position
        }

        // Apply force to all nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius); // Get all colliders within the explosion radius
        foreach (Collider2D collider in colliders) // Iterate through each collider found in the explosion radius
        {
            Rigidbody2D rb = collider.attachedRigidbody; // Get the Rigidbody2D component attached to the collider
            if (rb != null && rb != _rb) // Check if the Rigidbody2D is not null and exclude the bird's own Rigidbody2D from the explosion force
            {
                Vector2 direction = rb.position - _rb.position; // Calculate the direction from the bird to the object
                rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse); // Apply an impulse force in the direction of the explosion
            }
            //Damage nearby enemies
            collider.GetComponent<Enemy>()?.TakeDamage(999000666); // Deal massive damage to any enemy in the explosion radius
        }

        // Destroy the bird after explosion
        TriggerBirdStopped(); // Call the method to trigger the bird stopped event
        Destroy(gameObject); // Destroy the bird GameObject after the explosion
    }

    private void OnDrawGizmosSelected() 
    {
        // Visualize the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

