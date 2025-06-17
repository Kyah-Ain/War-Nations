using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using UnityEngine; // Grants access to Unity's core features 

public class GameManager : MonoBehaviour
{
    public int levelAt; // This identifies the player's current level

    // Start is called before the first frame update
    void Start()
    {
        // This updates the level save
        PlayerPrefs.SetInt("p_lastLevel", levelAt);

        if (PlayerPrefs.GetInt("p_levelAt") < levelAt) // Makes sure that the levelAt is always higher than the previous one
        {
            PlayerPrefs.SetInt("p_levelAt", levelAt); // Updates the highest level reached
        }
        PlayerPrefs.Save(); // Saves the PlayerPrefs data to the player's computer disk
        // End level save update
    }
}
