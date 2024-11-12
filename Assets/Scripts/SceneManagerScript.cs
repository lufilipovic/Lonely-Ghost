using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //public GameObject howToPlayPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // Ensure normal time scale at start
    }

    private void Update()
    {
        // Check if the 'P' key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle between pausing and resuming the game
            if (IsGamePaused())
            {
                ResumeGame();
                Debug.Log("Resuming game: " + Time.timeScale);
            }
            else
            {
                PauseGame();
                Debug.Log("Pausing game: " + Time.timeScale);
            }
        }
    }

    // Method to start playing the game (load next scene in build index)
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to exit the game
    public void ExitGame()
    {
        Debug.Log("Game Exited");
        Application.Quit(); // Quit the application
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Set time scale to 0 to pause the game
        //howToPlayPanel.SetActive(true); // Show the how-to-play panel
        Debug.Log("Pausing game: " + Time.timeScale);
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Set time scale back to 1 to resume the game
        //howToPlayPanel.SetActive(false); // Hide the how-to-play panel
        Debug.Log("Resuming game: " + Time.timeScale);
    }

    // Function to check if the game is currently paused
    private bool IsGamePaused()
    {
        return Time.timeScale == 0f; // Return true if time scale is 0 (game is paused)
    }

    // Method to load a specific scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
