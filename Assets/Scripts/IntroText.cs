using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI introText; // The TextMeshPro element for displaying text
    public GameObject nextButton; // Button for advancing text
    public float typingSpeed = 0.05f; // Speed of text typing effect

    [Header("Text Content")]
    [TextArea] public string[] storyChunks; // Array to hold chunks of the story

    private int currentChunkIndex = 0; // Track the current text chunk being displayed
    private bool isTyping = false; // Prevent skipping during typing

    public string sceneName;

    private void Start()
    {
        ShowNextChunk(); // Start displaying the first chunk
    }

    public void ShowNextChunk()
    {
        if (isTyping) return; // Prevent advancing while typing

        if (currentChunkIndex < storyChunks.Length)
        {
            StartCoroutine(TypeText(storyChunks[currentChunkIndex]));
            currentChunkIndex++;
        }
        else
        {
            // If all chunks are shown, load the main game
            LoadMainGame();
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        introText.text = ""; // Clear existing text
        foreach (char letter in text.ToCharArray())
        {
            introText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void SkipIntro()
    {
        LoadMainGame();
    }

    private void LoadMainGame()
    {
        SceneManager.LoadScene(sceneName); // Replace with your main game scene name
    }
}