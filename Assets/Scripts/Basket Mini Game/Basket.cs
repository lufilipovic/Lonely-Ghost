using UnityEngine;
using UnityEngine.EventSystems;

public class Basket : MonoBehaviour, IDropHandler
{
    [SerializeField] private string correctCandyTag; // Tag for the correct candy
    [SerializeField] private int totalCorrectCandyCount; // Total number of correct candies needed
    public GameObject miniGamePanel; // Reference to the mini-game panel
    private CandyCollection candyCollection; // Reference to the CandyCollection script

    private int correctCandyPlacedCount = 0; // Counter for correct candies placed

    private void Start()
    {
        // Find the CandyCollection script in the scene
        candyCollection = FindObjectOfType<CandyCollection>();

        if (candyCollection == null)
        {
            Debug.LogError("CandyCollection script not found in the scene!");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedCandy = eventData.pointerDrag;

        if (droppedCandy != null && droppedCandy.CompareTag(correctCandyTag))
        {
            Debug.Log("Correct candy placed!");

            //Increment the correct candy placed count
            correctCandyPlacedCount++;

            //// Add candy to the inventory
            //if (candyCollection != null)
            //{
            //    candyCollection.CollectCandy();
            //}

            // Destroy the dropped candy
            Destroy(droppedCandy);

            // Check if all correct candies are placed
            if (correctCandyPlacedCount >= totalCorrectCandyCount)
            {
                Debug.Log("All correct candies placed! Closing panel...");
                if (miniGamePanel != null)
                {
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
        }
    }
}