using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXDestroyer : MonoBehaviour
{
    private AudioSource audioInstance; // Reference to the AudioSource component

    private void Start()
    {
        audioInstance = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject (to skip manually assigning in the inspector)

        if (audioInstance != null)
        {
            Destroy(gameObject, audioInstance.clip.length); // Destroy the GameObject after the audio clip has finished playing
        }
        else 
        {
            Debug.LogWarning("AudioSource component not found on the GameObject.");
            Destroy(gameObject); // Destroy the GameObject immediately if no AudioSource is found
        }
    }
}
