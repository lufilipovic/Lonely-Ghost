using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueData dialogueData;

    private void OnMouseDown()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null && dialogueData != null)
        {
            dialogueManager.StartDialogue(dialogueData);
        }
    }
}
