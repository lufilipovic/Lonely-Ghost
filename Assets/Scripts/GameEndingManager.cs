using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameEndingManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject confirmationPanel; // The confirmation panel
    public TextMeshProUGUI endingText;   // Optional: Text to display the ending

    private CandyCollection candyCollection;// Number of friends the player has made (max 3)

    private bool isPlayerInRange = false; // Tracks if the player is in range

    public GameObject endingPanel;

    private void Start()
    {
        // Ensure the panel is initially inactive
        confirmationPanel.SetActive(false);
        endingPanel.SetActive(false);

        // Find the CandyCollection script in the scene
        candyCollection = FindObjectOfType<CandyCollection>();

        if (candyCollection == null)
        {
            Debug.LogError("CandyCollection script not found in the scene!");
        }
    }

    private void Update()
    {
        // Check for interaction input to open the intro panel
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) // Prevent reactivating intro
        {
            ShowConfirmationPanel();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure the collider is the player
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure the collider is the player
        {
            isPlayerInRange = false;

        }
    }

    public void ShowConfirmationPanel()
    {
        // Activate the confirmation panel
        confirmationPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void HideConfirmationPanel()
    {
        // Deactivate the confirmation panel
        confirmationPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void FinishGame()
    {
        // Deactivate the confirmation panel
        confirmationPanel.SetActive(false);
        Time.timeScale = 1f;


        // Determine the ending based on the number of friends
        if (candyCollection.friendCount == 3)
        {
            DisplayEnding("Congratulations! You’ve made 3 friends and helped Barnaboos find happiness!");
        }
        else if (candyCollection.friendCount == 2)
        {
            DisplayEnding("Great job! You’ve made 2 friends. Barnaboos is almost at peace!");
        }
        else if (candyCollection.friendCount == 1)
        {
            DisplayEnding("You’ve made 1 friend, but Barnaboos still longs for more companionship.");
        }
        else
        {
            DisplayEnding("Oh no! Barnaboos didn’t make any friends and remains in limbo.");
        }
    }

    private void DisplayEnding(string message)
    {
        endingPanel.SetActive(true);

        // Optional: If you have an ending text element in the scene, display the message
        if (endingText != null)
        {
            endingText.text = message;
        }

        // Use a coroutine for better control
        StartCoroutine(DelayedLoadEndingScene());
    }

    private IEnumerator DelayedLoadEndingScene()
    {
        Time.timeScale = 1f; 
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        SceneManager.LoadScene("GameOver"); // Replace with your actual scene name
    }
}