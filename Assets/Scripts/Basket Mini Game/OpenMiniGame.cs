using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpenMiniGame : MonoBehaviour
{
    public GameObject introPanel;       // Reference to the intro panel for the conversation
    public TextMeshProUGUI dialogueText; // Text component for dialogue display
    public Button nextButton;           // Button to progress the conversation
    public GameObject miniGamePanel;    // Reference to the mini-game panel
    public GameObject interactPrompt;   // Reference to the UI prompt
    private bool isPlayerInRange = false; // Tracks if the player is in range
    private bool hasShownIntro = false; // Tracks if the intro panel has already been shown

    [TextArea] public string[] dialogueLines; // Array of dialogue lines
    private int currentLineIndex = 0;         // Tracks the current line of dialogue

    private void Start()
    {
        // Ensure the panels and prompt are set up correctly
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (introPanel != null) introPanel.SetActive(false);
        if (miniGamePanel != null) miniGamePanel.SetActive(false);

        // Assign button click listener for progressing dialogue
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(ShowNextLine);
        }
    }

    private void Update()
    {
        // Check for interaction input to open the intro panel
        if (isPlayerInRange && Input.GetMouseButtonDown(0) && !hasShownIntro) // Prevent reactivating intro
        {
            OpenIntroPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure the collider is the player
        {
            isPlayerInRange = true;

            // Show the interact prompt
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure the collider is the player
        {
            isPlayerInRange = false;

            // Hide the interact prompt
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
    }

    private void OpenIntroPanel()
    {
        if (introPanel != null)
        {
            introPanel.SetActive(true); // Show the intro panel

            // Ensure the current line index is within bounds
            if (dialogueLines.Length > 0 && currentLineIndex < dialogueLines.Length)
            {
                DisplayLine(); // Show the first line of dialogue
            }
            else
            {
                Debug.LogWarning("No dialogue lines available or index out of range.");
            }

            Time.timeScale = 0f; // Pause the game
            hasShownIntro = true; // Mark intro as shown
        }

        // Hide the interact prompt
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    private void ShowNextLine()
    {
        currentLineIndex++;

        // Check if we have more lines to display
        if (currentLineIndex < dialogueLines.Length)
        {
            DisplayLine(); // Show the next line
        }
        else
        {
            EndConversation(); // End the conversation
        }
    }

    private void DisplayLine()
    {
        // Safeguard against index out of range
        if (currentLineIndex >= 0 && currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex]; // Update the dialogue text
        }
        else
        {
            Debug.LogError("Attempted to access a dialogue line outside of valid range.");
        }
    }

    private void EndConversation()
    {
        if (introPanel != null) introPanel.SetActive(false); // Hide the intro panel
        if (miniGamePanel != null) miniGamePanel.SetActive(true); // Show the mini-game panel
        Time.timeScale = 1f; // Resume the game
    }
}