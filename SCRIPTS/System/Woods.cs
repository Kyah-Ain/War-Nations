using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 

public class Woods : Damageable
{
    public int setHealth = 0; // Optional health value

    void Start()
    {
        if (setHealth <= 0) // If no health is set, initialize with a default value
        {
            // Initialize the object's health or other properties if needed
            health = 150f; // Base Health 
        }
        else 
        {
            health = setHealth; // Set the health to the specified value
        }
    }

    protected override void Die() // This method is called when the wood is destroyed
    {
        base.Die(); // Call the base class Die method
        starManager.scorePoints += 2500; // Add points to the score when the wood is destroyed
    }
}
