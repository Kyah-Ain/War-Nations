using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 

public class SpainShipBird : Bird
{
    public float dashForce = 20f; // Customize in the inspector
    public float gravityScale = 2f; // Customize in the inspector
    private bool hasDashed = false; // Flag to check if the bird has already dashed

    void Update()
    {
        base.Update(); // Keep the stopped-check logic

        if (_launched && !hasDashed && Input.GetMouseButtonDown(0)) // Check for mouse click to trigger dash
        {
            Dash(); // Call the Dash method when the mouse button is pressed
        }
    }

    public override void Launch(Vector2 force) // Override the Launch method to set the gravity scale and apply the force
    {
        _rb.gravityScale = gravityScale; // Set gravity scale for this bird type
        base.Launch(force); // Call the base Launch method to apply the force
    }

    private void Dash() // Method to handle the dash logic
    {
        hasDashed = true; // Set the flag to true to prevent multiple dashes

        // Apply dash in the current velocity direction
        if (_rb.velocity.magnitude > 0.1f)
        {
            Vector2 dashDirection = _rb.velocity.normalized; // Get the normalized direction of the current velocity
            _rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse); // Apply an impulse force in the dash direction
        }
    }
}