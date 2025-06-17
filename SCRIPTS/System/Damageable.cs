using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 

public class Damageable : MonoBehaviour 
{
    // ------------------------- VARIABLES -------------------------
    public StarManager starManager; // Reference to the StarManager.cs script

    public float health = 30f;
    public GameObject deathEffect; // Optional: assign particle or visual effect
    public GameObject deathSFX; // Optional: assign sound effect

    protected bool isDead = false; // Flag to check if the object is dead

    // -------------------------- METHODS --------------------------
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return; // If already dead, ignore further collisions

        float impactForce = collision.relativeVelocity.magnitude; // Calculate the impact force based on collision velocity

        // Example: cause damage based on force (customize this logic)
        if (impactForce > 2f)
        {
            TakeDamage(impactForce * 5f); // Scale damage with force
        } 
    }

    public virtual void TakeDamage(float amount) // Method to apply damage to the object
    {
        if (isDead) return; // If already dead, ignore further damage

        health -= amount; // Subtract the damage amount from health
        if (health <= 0f) // If health drops to zero or below, trigger death
        {
            Die(); // Call the Die method to handle death logic
        }
    }

    protected virtual void Die() // Method to handle the death of the object
    {
        isDead = true; // Sets the object as dead for it to despawn

        // Plays the death animation 
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity); // Instantiate the death effect at the object's position
        }

        // Plays the death sound effect
        if (deathSFX != null) 
        {
            Instantiate(deathSFX, transform.position, Quaternion.identity); // Instantiate the death sound effect at the object's position
        }

        // Notify game manager (optional, for scoring or tracking)
        // GameManager.Instance.OnEnemyKilled();

        gameObject.SetActive(false); // Deactivate the object instead of destroying it, to allow for pooling or reuse
    }
}
