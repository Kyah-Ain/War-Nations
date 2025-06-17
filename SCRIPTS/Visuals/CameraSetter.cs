using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    // ------------------------- VARIABLES -------------------------
    public SpriteRenderer targetSize; // The target size of the camera

    // -------------------------- METHODS --------------------------
    void Start() // Start is called before the first frame update
    {
        float screenRatio = (float)Screen.width / (float)Screen.height; // Calculate the screen ratio
        float targetRatio = targetSize.bounds.size.x / targetSize.bounds.size.y; // Calculate the target ratio

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = targetSize.bounds.size.y / 2; // Set the camera size based on the target size
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio; // Calculate the difference in size
            Camera.main.orthographicSize = targetSize.bounds.size.y / 2 * differenceInSize; // Adjust the camera size based on the difference
        }
    }

}
