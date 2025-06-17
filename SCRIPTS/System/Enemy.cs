using Unity.VisualScripting; // Grants access to Visual Scripting features, if needed
using UnityEngine; // Grants access to Unity's core features 

public class Enemy : Damageable
{
    // ------------------------- VARIABLES -------------------------
    public int setHealth = 0; // Optional health value
    public static int enemyCount; // tracks the number of enemies
    bool isAnEnemy = false; // Flag to check if the object is an enemy

    // -------------------------- METHODS --------------------------
    void Start()
    {
        if (setHealth <= 0) // If no health is set, initialize with a default value
        {
            // Initialize the object's health or other properties if needed
            health = 50f; // Base Health 
        }
        else
        {
            health = setHealth; // Set the health to the specified value
        }

        enemyCount++; // Increment the enemy count when the enemy is created
        Debug.Log($"{enemyCount}");
    }

    protected override void OnCollisionEnter2D(Collision2D collision) // This method is called when the enemy collides with another object
    {
        if (gameObject.CompareTag("Enemy"))
        {
            isAnEnemy = true; // Check if the object is an enemy
            base.OnCollisionEnter2D(collision); // Call the base class method
        }
    }

    protected override void Die() // This method is called when the enemy dies
    {
        if (isAnEnemy && enemyCount > 0) // Check if the object is an enemy and there are enemies left
        {
            enemyCount--; // Decrement the enemy count when the enemy dies
            starManager.scorePoints += 1250; // Add points to the score when the enemy is destroyed

            Debug.Log($"{enemyCount}");
            isAnEnemy = false; // Reset the isAnEnemy flag
        }

        base.Die(); // Call the base class Die method

        if (enemyCount < 1 && starManager.isGameComplete == false) // Check if all enemies are defeated and the game is not already complete
        {
            starManager.ShowWinPanel(); // Show the floating panel when the last enemy dies
            starManager.isGameComplete = true; // Set the game as complete
            enemyCount = 0; // Should be outside this script
        }
        
    }
}