using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCRiddles : MonoBehaviour
{
    [Header("Riddle Settings")]
    [TextArea] public string riddleText;      // Riddle question text
    public string[] answers = new string[3];  // Three answer options
    public int correctAnswerIndex;            // Index of the correct answer (0, 1, or 2)

    [Header("UI References")]
    public GameObject riddlePanel;            // Panel to display the riddle and buttons
    public TextMeshProUGUI riddleDisplayText;            // Text to show the riddle question
    public Button[] answerButtons;            // Buttons for each answer option

    private GameObject player; // Reference to the player
    public float playerRange = 2f;

    private bool hasAnsweredCorrectly = false;

    void Start()
    {
        riddlePanel.SetActive(false);  // Hide the riddle panel initially
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(!hasAnsweredCorrectly && IsPlayerInRange() && Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            ShowRiddle();
        }
    }
    public void ShowRiddle()
    {
        riddlePanel.SetActive(true);
        riddleDisplayText.text = riddleText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
            answerButtons[i].onClick.RemoveAllListeners();  // Clear previous listeners
            int index = i;  // Capture index for button listener
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
            riddlePanel.SetActive(false);
        }
        else
        {
            Debug.Log("Incorrect answer.");
        }

        // Close the riddle panel and reset buttons
        riddlePanel.SetActive(false);
        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    // Check if the player is within pickup range
    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= playerRange;
    }
}
