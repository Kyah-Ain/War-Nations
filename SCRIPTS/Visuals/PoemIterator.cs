using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using TMPro; // Grants access to Text Mesh Pro functions
using UnityEngine; // Grants access to Unity's core features
using UnityEngine.UI; // Allows the use of UI elements, such as Image components

public class PoemIterator : MonoBehaviour
{
    [Header("Reference")]
    public GameObject backButton; // The back button that can be clicked in the UI
    public TextMeshProUGUI outroMessage; // The TextMeshProUGUI component that displays the poem lines

    [Header("Dialogue")]
    public string[] lines; // Array of strings containing the lines of the poem to be displayed
    public float textSpeed; // Speed at which each character of the poem is displayed

    [Header("Regulator")]
    public int indexCounter; // Counter to track the current line being displayed in the poem
    public float timeBeforeNextDialogue = 0; // Time to wait before displaying the next line of the poem
    public static bool isButtonUnlocked = false; // Flag to check if the skip button is unlocked

    void Start() // Start is called before the first frame update
    {
        StartCoroutine(SkipButtonUnlock()); // Starts the coroutine to unlock the skip button after displaying the poem
    }

    IEnumerator SkipButtonUnlock() // Iterates through the poem lines and unlocks the skip button after the dialogue is complete
    {
        if (isButtonUnlocked == true) // Checks if the button is already unlocked before
        {
            backButton.SetActive(true); // Activates the back button if it is already unlocked before
        }

        outroMessage.text = string.Empty; // Clears the text field before starting the dialogue
        yield return StartCoroutine(DialogueIteration()); // Starts the poem
        isButtonUnlocked = true; // Sets the flag to true, indicating the button is now unlocked
        backButton.SetActive(true); // Activates the back button after the dialogue is complete

        Debug.Log($"isButtonUnlocked: {isButtonUnlocked}");
    }

    IEnumerator DialogueIteration() // Iterates through the sentences of the poem and displays them per lines
    {
        for (int i = 0; i < lines.Length; i++)
        {
            yield return StartCoroutine(TextIterator()); // Calls the TextIterator coroutine to display each character of the current line
            yield return new WaitForSeconds(timeBeforeNextDialogue); // Waits for a specified time before displaying the next line
            outroMessage.text = string.Empty; // Clears the text field before displaying the next line
        }
    }

    IEnumerator TextIterator() // Iterates through the characters of the current line and displays them word per word
    {
        foreach (char c in lines[indexCounter].ToCharArray())
        {
            outroMessage.text += c; // Appends the current character to the text field prompt

            yield return new WaitForSeconds(textSpeed); // Waits for the specified text speed before displaying the next character
        }
        indexCounter++; // Increments the index counter to move to the next line of the poem
    }
}
