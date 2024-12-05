using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName; // Optional: Speaker's name.
        [TextArea(2, 5)] public string dialogueText;
    }

    public DialogueLine[] dialogueLines;
}
