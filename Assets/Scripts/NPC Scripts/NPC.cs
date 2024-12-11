using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("NPC Components")]
    public DialogueData dialogueData; // Dialogue content for the NPC
    public NPCRiddles npcRiddles; // Reference to the NPCRiddles script for this NPC

    [Header("Player Interaction")]
    public float interactionRange = 2f; // Range within which the player can interact
    private bool playerIsClose = false; // Tracks if the player is within range
    private bool hasTriggeredDialogue = false; // Tracks if the dialogue has been triggered

    private GameObject player; // Reference to the player GameObject

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Ensure the NPCRiddles script is assigned
        if (npcRiddles == null)
        {
            npcRiddles = GetComponent<NPCRiddles>();
        }

        // Ensure the riddle panel starts hidden
        if (npcRiddles != null)
        {
            npcRiddles.riddlePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the player is within interaction range
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            playerIsClose = distance <= interactionRange;
        }
    }

    private void OnMouseDown()
    {
        if (!playerIsClose || hasTriggeredDialogue) return;

        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null && dialogueData != null)
        {
            dialogueManager.StartDialogue(dialogueData);
            dialogueManager.OnDialogueFinished += HandlePostDialogue; // Subscribe to dialogue end event
            hasTriggeredDialogue = true; // Mark dialogue as triggered
        }
        else
        {
            Debug.LogError("DialogueManager or DialogueData is missing.");
        }
    }

    private void HandlePostDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueFinished -= HandlePostDialogue; // Unsubscribe from the event
        }

        // Show riddle panel if there's a riddle and it hasn't been answered
        if (npcRiddles != null && !npcRiddles.HasAnsweredCorrectly)
        {
            npcRiddles.PrepareRiddle();
            return; // Stop further execution to prevent showing candy choice panel
        }

        // Show candy choice panel if no riddle or riddle is already answered
        TriggerCandyChoice();
    }

    private void TriggerCandyChoice()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.candyChoicePanel.SetActive(true); // Show the candy choice panel
        }
    }

    public void ResetDialogueState()
    {
        if (npcRiddles != null && !npcRiddles.HasAnsweredCorrectly)
        {
            // Keep the dialogue triggered if the riddle hasn't been answered
            return;
        }

        hasTriggeredDialogue = false; // Allow the player to reinitiate dialogue
    }
}