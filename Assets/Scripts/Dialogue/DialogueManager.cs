using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialogueBox; // The UI panel for dialogue
    public TextMeshProUGUI dialogueText; // Text element to display dialogue
    public TextMeshProUGUI speakerNameText; // Optional: Speaker's name

    [Header("Player Control")]
    public PlayerController playerController; // Reference to the player control script

    [Header("Candy Decision")]
    public GameObject candyChoicePanel; // Panel for candy decision
    public GameObject successPanel; // Panel for "Yes and enough candy"
    public GameObject insufficientCandyPanel; // Panel for "Yes but not enough candy"
    public GameObject noCandyGivenPanel; // Panel for "No candy given"

    private CandyCollection inventoryManager; // Reference to the inventory manager

    private DialogueData currentDialogue;
    private int currentLineIndex;
    private bool isDialogueActive;

    public delegate void DialogueFinished();
    public event DialogueFinished OnDialogueFinished;

    private bool triggerCandyChoice = false; // Flag to control candy choice panel
    private float panelDisplayDuration = 3f; // Duration to show panels

    private void Start()
    {
        // Find the CandyCollection script in the scene
        inventoryManager = FindObjectOfType<CandyCollection>();

        if (inventoryManager == null)
        {
            Debug.LogError("CandyCollection script not found in the scene!");
        }
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0)) // 0 = left mouse button
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(DialogueData dialogue, bool showCandyChoice = false)
    {
        if (isDialogueActive)
        {
            Debug.LogWarning("Dialogue is already active. Ignoring new dialogue request.");
            return;
        }

        if (playerController != null)
        {
            playerController.enabled = false; // Disable player controls
        }

        triggerCandyChoice = showCandyChoice; // Set flag for candy choice panel

        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialogueBox.SetActive(true);
        isDialogueActive = true;
        Time.timeScale = 0f; // Pause the game

        DisplayCurrentLine(); // Display the first line
    }

    private void DisplayCurrentLine()
    {
        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            var line = currentDialogue.dialogueLines[currentLineIndex];
            if (speakerNameText != null) speakerNameText.text = line.speakerName;
            dialogueText.text = line.dialogueText;
        }
    }

    public void DisplayNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex < currentDialogue.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (playerController != null)
        {
            playerController.enabled = true; // Re-enable player controls
        }

        dialogueBox.SetActive(false);
        isDialogueActive = false;
        Time.timeScale = 1f; // Resume the game

        OnDialogueFinished?.Invoke();

        currentDialogue = null;
        currentLineIndex = 0;

        // Only show the candy choice panel if the flag is true
        if (triggerCandyChoice)
        {
            candyChoicePanel.SetActive(true);
        }
    }

    public void HandleCandyChoice(bool giveCandy)
    {
        candyChoicePanel.SetActive(false); // Close the candy choice panel

        if (giveCandy)
        {
            if (inventoryManager != null && inventoryManager.candyCount >= 5)
            {
                inventoryManager.MakeFriend();
                ShowPanel(successPanel); // Show success panel
                Debug.Log("Player gave 5 candies.");
            }
            else
            {
                ShowPanel(insufficientCandyPanel); // Show insufficient candy panel
                Debug.Log("Not enough candies!");

                // Reset NPC state for retry
                ResetNPCState();
            }
        }
        else
        {
            ShowPanel(noCandyGivenPanel); // Show no candy given panel
            Debug.Log("Player chose not to give candies.");
        }
    }

    private void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Invoke(nameof(HidePanels), panelDisplayDuration); // Automatically hide after a few seconds
        }
    }

    private void HidePanels()
    {
        successPanel?.SetActive(false);
        insufficientCandyPanel?.SetActive(false);
        noCandyGivenPanel?.SetActive(false);
    }

    private void ResetNPCState()
    {
        // Find all NPCs in the scene
        NPC[] npcs = FindObjectsOfType<NPC>();
        foreach (NPC npc in npcs)
        {
            npc.ResetDialogueState(); // Reset each NPC's state
        }
    }
}