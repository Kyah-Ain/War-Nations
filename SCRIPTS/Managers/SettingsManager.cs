using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 

public class SettingsManager : MonoBehaviour
{
    [Header("Reference")]
    public StarManager starManager; // Reference to the StarManager.cs script

    [Header("Panel Reference")]
    public GameObject toOpen; // Reference to the GameObject to open
    public GameObject toClose; // Reference to the GameObject to close

    public void OpenPanel() // Opens settings panel
    {
        if (starManager.isGameComplete == false) // Allow players to access settings only when the level is not yet complete
        {
            Time.timeScale = 0f; // Pauses the game
            toOpen.SetActive(true); // Activates the settings panel
        }
    }

    public void ClosePanel() // Closes settings panel
    {
        Time.timeScale = 1f; // Resumes the game
        toClose.SetActive(false); // Deactivates the settings panel
    }
}
