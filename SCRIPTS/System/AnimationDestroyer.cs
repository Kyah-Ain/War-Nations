using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroyer : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject (to skip manually assigning in the inspector)

        if (animator != null && animator.runtimeAnimatorController != null)
        {
            // Get the first/default animation clip
            AnimationClip defaultClip = animator.runtimeAnimatorController.animationClips[0];

            if (defaultClip != null)
            {
                Destroy(gameObject, defaultClip.length); // Destroy after default animation ends
            }
            else
            {
                Debug.LogWarning("No animation clip found. Destroying immediately.");
                Destroy(gameObject);
            }
        }

        else
        {
            Debug.LogWarning("Animator or Controller not found. Destroying immediately.");
            Destroy(gameObject);
        }
    }
}
