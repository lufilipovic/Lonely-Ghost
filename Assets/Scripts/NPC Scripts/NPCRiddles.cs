using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCRiddles : MonoBehaviour
{
    [Header("Riddle Settings")]
    [TextArea] public string riddleText; // The riddle text specific to this NPC
    public string[] answers = new string[3]; // The possible answers specific to this NPC
    public int correctAnswerIndex; // The index of the correct answer for this NPC

    [Header("Shared UI References")]
    public GameObject riddlePanel; // Shared riddle panel
    public TextMeshProUGUI riddleDisplayText; // Shared riddle text display
    public Button[] answerButtons; // Shared answer buttons
    public GameObject correctAnswerPanel; // Shared correct answer panel
    public GameObject wrongAnswerPanel; // Shared wrong answer panel
    public Button restartButton; // Restart button on the wrong answer panel
    public Button exitButton; // Exit button on the correct answer panel

    private bool hasAnsweredCorrectly = false; // Tracks if the riddle has been answered correctly

    public bool HasAnsweredCorrectly => hasAnsweredCorrectly; // Public getter for encapsulation

    private CandyCollection candyCollection; // Reference to the CandyCollection script

    private void Start()
    {
        // Find the CandyCollection script in the scene
        candyCollection = FindObjectOfType<CandyCollection>();

        if (candyCollection == null)
        {
            Debug.LogError("CandyCollection script not found in the scene!");
        }
        // Ensure shared panels are hidden initially
        riddlePanel.SetActive(false);
        correctAnswerPanel.SetActive(false);
        wrongAnswerPanel.SetActive(false);

        // The Restart button listener is dynamically assigned when showing the riddle
        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(CloseCorrectPanel);
        }
    }

    public void PrepareRiddle()
    {
        if (!hasAnsweredCorrectly)
        {
            ShowRiddle();
        }
    }

    private void ShowRiddle()
    {
        Debug.Log($"Showing riddle for NPC: {gameObject.name}");

        // Dynamically update the Restart button listener for this NPC
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartRiddle); // Assign RestartRiddle for this NPC
        }

        // Update shared panel content for this NPC
        riddleDisplayText.text = riddleText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
            answerButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            int index = i; // Capture current index for closure
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }

        // Display the shared panel
        riddlePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    private void CheckAnswer(int chosenIndex)
    {
        if (chosenIndex == correctAnswerIndex)
        {
            Debug.Log("Correct! Candy added.");
            hasAnsweredCorrectly = true;
            correctAnswerPanel.SetActive(true);
            candyCollection.CollectCandy();

        }
        else
        {
            Debug.Log("Incorrect answer.");
            wrongAnswerPanel.SetActive(true);
        }

        riddlePanel.SetActive(false); // Hide the riddle panel
        Time.timeScale = 1f; // Resume the game
    }

    private void RestartRiddle()
    {
        Debug.Log($"Restarting riddle for NPC: {gameObject.name}");

        // Hide the wrong answer panel
        wrongAnswerPanel.SetActive(false);

        // Reuse the shared panel to show the same riddle again
        ShowRiddle();
    }

    public void CloseCorrectPanel()
    {
        correctAnswerPanel.SetActive(false);
    }
}