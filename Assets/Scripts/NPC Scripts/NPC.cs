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
            dialogueManager.OnDialogueFinished += TriggerRiddle; // Subscribe to dialogue end event
            hasTriggeredDialogue = true; // Mark dialogue as triggered
        }
        else
        {
            Debug.LogError("DialogueManager or DialogueData is missing.");
        }
    }

    private void TriggerRiddle()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueFinished -= TriggerRiddle; // Unsubscribe from the event
        }

        if (npcRiddles != null && !npcRiddles.HasAnsweredCorrectly)
        {
            npcRiddles.PrepareRiddle(); // Prepare and show the riddle
        }
    }
}