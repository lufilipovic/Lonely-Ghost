using UnityEngine;
using UnityEngine.EventSystems;

public class Basket : MonoBehaviour, IDropHandler
{
    [SerializeField] private string correctCandyTag; // Tag for the correct candy
    [SerializeField] private int totalCorrectCandyCount; // Total number of correct candies needed
    public GameObject miniGamePanel; // Reference to the mini-game panel
    private CandyCollection candyCollection; // Reference to the CandyCollection script

    private int correctCandyPlacedCount = 0; // Counter for correct candies placed

    [Header("Audio Clips")]
    public AudioClip correctCandySound; // Sound for correct candy placement
    public AudioClip incorrectCandySound; // Sound for incorrect candy placement

    private ItemSoundManager soundManager; // Reference to the centralized ItemSoundManager

    private void Start()
    {
        // Find the CandyCollection script in the scene
        candyCollection = FindObjectOfType<CandyCollection>();
        if (candyCollection == null)
        {
            Debug.LogError("CandyCollection script not found in the scene!");
        }

        // Find the centralized sound manager
        soundManager = FindObjectOfType<ItemSoundManager>();
        if (soundManager == null)
        {
            Debug.LogError("Centralized ItemSoundManager not found in the scene!");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedCandy = eventData.pointerDrag;

        if (droppedCandy != null && droppedCandy.CompareTag(correctCandyTag))
        {
            Debug.Log("Correct candy placed!");

            // Increment the correct candy placed count
            correctCandyPlacedCount++;

            // Play correct candy sound
            if (soundManager != null && correctCandySound != null)
            {
                soundManager.PlaySpecificSound(correctCandySound);
            }

            // Destroy the dropped candy
            Destroy(droppedCandy);

            // Check if all correct candies are placed
            if (correctCandyPlacedCount >= totalCorrectCandyCount)
            {
                Debug.Log("All correct candies placed! Closing panel...");
                if (miniGamePanel != null)
                {
                    candyCollection.CollectCandy();
                    candyCollection.CollectCandy();
                    miniGamePanel.SetActive(false);
                }
                else
                {
                    Debug.LogError("MiniGamePanel is not assigned!");
                }
            }
        }
        else
        {
            Debug.Log("Incorrect candy.");
            // Play incorrect candy sound
            if (soundManager != null && incorrectCandySound != null)
            {
                soundManager.PlaySpecificSound(incorrectCandySound);
            }
        }
    }
}