using System.Collections; // Grants access to collections and data structures like ArrayList
using System.Collections.Generic; // Grants access to collections and data structures like List and Dictionary
using TMPro; // Grants access to Text Mesh Pro functions
using UnityEngine.UI; // Allows the use of UI elements, such as Image components
using UnityEngine; // Grants access to Unity's core features 

public class StarManager : MonoBehaviour
{
    // ------------------------- VARIABLES -------------------------
    [Header("UI Panel Visuals")]

    public GameObject floatingWinPanel; // Refers to the Win Floating Panel in the Game Scene
    public GameObject floatingLosePanel; // Refers to the Lose Floating Panel in the Game Scene

    public TextMeshProUGUI winPrompt; // Refers to the Win Prompt in the Game Scene
    public TextMeshProUGUI displayedScore; // Refers to the Score Board in the Game Scene
    public TextMeshProUGUI currentBirdsLeft; // Refers to the Number of Birds Left in the Game Scene

    public Image oneStar; // Reference to the first star image for one star
    public Image secondStar; // Reference to the second star image for two stars
    public Image thirdStar; // Reference to the third star image for three stars

    private Color filledStarColor; // Color for earned stars (Goldish Yellow)
    private Color blankStarColor;  // Color for unearned stars (Dark Gray)


    [Header("Game Regulators")]

    public int oneStarThreshold = 0; // Threshold to earn one star
    public int twoStarThreshold = 20000; // Threshold to earn two stars
    public int threeStarThreshold = 30000; // Threshold to earn three stars

    public int numberOfBirdsLeft; // Keeps track of the number of birds left in the game (used for the Regulator)
    public int scorePoints; // Keeps track of the points earned by the player
    public int currentLevelAt; // Stores what current level you are and base the display format on the current level

    public bool isGameComplete = false; // Flag to check if the game is complete

    // ------------------------- FUNCTIONS -------------------------
    private void Awake()
    {
        // Convert hex strings to Unity colors
        ColorUtility.TryParseHtmlString("#FFFFFF", out filledStarColor); // Sets the color of filled stars to Goldish Yellow
        ColorUtility.TryParseHtmlString("#656565", out blankStarColor);  // Sets the color of blank stars to Dark Gray
    }

    private void Start()
    {
        isGameComplete = false; // Sets the rule of the game back to default
    }

    void Update() // Update is called once per frame 
    {
        displayedScore.text = $"{scorePoints}"; // Updates the current coin score in the Game Scene RealTime
        currentBirdsLeft.text = $"x {numberOfBirdsLeft}"; // Updates the current number of birds left in the Game Scene RealTime
    }

    public void ShowWinPanel() // Panel for when the player WINS the game
    {
        for (int i = 0; i < numberOfBirdsLeft; i++) // Turns the number of birds left into points
        {
            scorePoints += 20000; // Points for each bird left
            Debug.Log($"Points for birds left: {scorePoints}");
        }

        floatingWinPanel.SetActive(true); // Activates the WIN panel

        // Score Thresholds for Stars Acquired
        if (scorePoints >= threeStarThreshold)
        {
            winPrompt.text = $"Wow! You're an amazing killer!"; // Sets the win prompt text for 3 stars
            oneStar.color = filledStarColor; // Sets the first star to Goldish Yellow
            secondStar.color = filledStarColor; // Sets the second star to Goldish Yellow
            thirdStar.color = filledStarColor; // Sets the third star to Goldish Yellow
            Debug.Log("You got 3 stars!"); 
        }
        else if (scorePoints >= twoStarThreshold)
        {
            winPrompt.text = $"You almost ruled the world!"; // Sets the win prompt text for 2 stars
            oneStar.color = filledStarColor; // Sets the first star to Goldish Yellow
            secondStar.color = filledStarColor; // Sets the second star to Goldish Yellow
            thirdStar.color = blankStarColor; // Sets the third star to blank Dark Gray
            Debug.Log("You got 2 stars!"); 
        }
        else if (scorePoints >= oneStarThreshold)
        {
            winPrompt.text = $"Not bad! At least you got rid of everyone!"; // Sets the win prompt text for 1 star
            oneStar.color = filledStarColor; // Sets the first star to Goldish Yellow
            secondStar.color = blankStarColor; // Sets the second star to blank Dark Gray
            thirdStar.color = blankStarColor; // Sets the third star to blank Dark Gray
            Debug.Log("You got 1 star!"); 
        }
        else
        {
            winPrompt.text = $"Aww, I get it! That soft heart of yours is holding you back, haha!"; // Sets the win prompt text for 0 star
            oneStar.color = blankStarColor; // Sets the first star to blank Dark Gray
            secondStar.color = blankStarColor; // Sets the second star to blank Dark Gray
            thirdStar.color = blankStarColor; // Sets the third star to blank Dark Gray
            Debug.Log("You got 0 stars!"); 
        }
    }

    public void ShowLosePanel() // Panel for when the player LOSE the game
    {
        floatingLosePanel.SetActive(true); // Activates the LOSE panel
        Debug.Log("GameOver ka lods, haha lamao.");
    }
}
