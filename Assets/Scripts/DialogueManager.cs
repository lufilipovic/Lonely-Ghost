using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox; // The UI panel for dialogue.
    public TextMeshProUGUI dialogueText; // Text element to display dialogue.
    public TextMeshProUGUI speakerNameText; // Optional: Speaker's name.

    private DialogueData currentDialogue;
    private int currentLineIndex;
    private bool isDialogueActive;

    public PlayerController playerController; // Reference to player control script

    public delegate void DialogueFinished();
    public event DialogueFinished OnDialogueFinished;

    void Update()
    {
        // Check for mouse click to proceed dialogue
        if (isDialogueActive && Input.GetMouseButtonDown(0)) // 0 = left mouse button
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(DialogueData dialogue)
    {
        // Disable player controls
        playerController.enabled = false;

        currentDialogue = dialogue;
        currentLineIndex = 0; // Start at the first line
        dialogueBox.SetActive(true);
        isDialogueActive = true;

        // Pause the game
        Time.timeScale = 0f;

        DisplayCurrentLine(); // Display the first line immediately
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
        currentLineIndex++; // Increment to the next line

        // Check if there are more lines to display
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
        // Enable player controls
        playerController.enabled = true;

        dialogueBox.SetActive(false);
        isDialogueActive = false;
        Time.timeScale = 1f;

        // Notify listeners that dialogue has finished
        OnDialogueFinished?.Invoke();

        currentDialogue = null;
        currentLineIndex = 0;
    }
}
