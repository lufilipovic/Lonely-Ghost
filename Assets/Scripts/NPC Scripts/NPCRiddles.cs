using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCRiddles : MonoBehaviour
{
    [Header("Riddle Settings")]
    [TextArea] public string riddleText;
    public string[] answers = new string[3];
    public int correctAnswerIndex;

    [Header("UI References")]
    public GameObject riddlePanel;
    public TextMeshProUGUI riddleDisplayText;
    public Button[] answerButtons;

    private GameObject player;
    public float playerRange = 2f;

    private bool hasAnsweredCorrectly = false;
    private bool riddleReadyToShow = false;

    public GameObject correctAnswerPanel;
    public GameObject wrongAnswerPanel;
    public Button restartButton; // Reference to the Restart button on the wrongAnswerPanel
    public Button exitButton;

    void Start()
    {
        riddlePanel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");

        // Subscribe to the DialogueManager's event
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueFinished += PrepareRiddle;
        }

        // Assign the Restart button functionality
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartRiddle);
        }

        if(exitButton != null)
        {
            exitButton.onClick.AddListener(CloseCorrectPanel);
        }
    }

    private void Update()
    {
        if (riddleReadyToShow && IsPlayerInRange() && !hasAnsweredCorrectly)
        {
            ShowRiddle();
            riddleReadyToShow = false;
        }
    }

    public void PrepareRiddle()
    {
        riddleReadyToShow = true;
    }

    public void ShowRiddle()
    {
        Time.timeScale = 0f;
        riddlePanel.SetActive(true);
        riddleDisplayText.text = riddleText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            int index = i;
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    public void CheckAnswer(int chosenIndex)
    {
        if (chosenIndex == correctAnswerIndex)
        {
            CandyCollection candyCollector = player.GetComponent<CandyCollection>();
            candyCollector.CollectCandy();
            Debug.Log("Correct! Candy added.");
            hasAnsweredCorrectly = true;
            correctAnswerPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Incorrect answer.");
            wrongAnswerPanel.SetActive(true);
        }

        ResumeGame(); // Resume the game after answering
    }

    private void RestartRiddle()
    {
        // Hide the wrongAnswerPanel
        wrongAnswerPanel.SetActive(false);

        // Reset and re-show the riddle panel
        ShowRiddle();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        riddlePanel.SetActive(false);

        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= playerRange;
    }

    private void OnDestroy()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueFinished -= PrepareRiddle;
        }
    }

    public void CloseCorrectPanel()
    {
        correctAnswerPanel.SetActive(false);
    }
}