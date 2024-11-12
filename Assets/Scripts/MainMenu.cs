using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour

{
    public GameObject instructionsPanel;
    public Button instructionsButton;
    public Button playButton;
    public Button quitButton;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    // Play game function - OnCLick
    public void PlayGame()
    {
        SceneManager.LoadScene("TrickOrTreatScene");
    }
    // SetActive instructions Panel 
    public void InstructionsOn()
    {
        instructionsPanel.SetActive(true);
        instructionsButton.interactable = false;
        playButton.interactable = false;
        quitButton.interactable = false;
    }

    public void InstructionsOff()
    {
        instructionsPanel.SetActive(false);
        instructionsButton.interactable = true;
        playButton.interactable = true;
        quitButton.interactable = true;


    }
    // Quit game function - OnClick
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void RestartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        //dodat panel za pauziranje igre
        Debug.Log("Game Paused!");
    }
}
