using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private string matchingTag; // Tag for the correct shape
    public GameObject shapesPanel; // Panel to close after matching shapes
    public int totalShapes; // Total number of shapes to match

    private int correctShapesCount = 0; // Counter for correct shapes placed
    private CandyCollection candyCollection; // Reference to the CandyCollection script

    private ItemSoundManager soundManager; // Reference to the centralized ItemSoundManager

    [Header("Audio Clips")]
    public AudioClip correctCandySound; // Sound for correct candy placement
    public AudioClip incorrectCandySound; // Sound for incorrect candy placement

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
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null && droppedObject.CompareTag(matchingTag))
        {
            // Snap the object to the drop zone
            RectTransform dropZoneTransform = GetComponent<RectTransform>();
            RectTransform objectTransform = droppedObject.GetComponent<RectTransform>();
            if (dropZoneTransform != null && objectTransform != null)
            {
                objectTransform.anchoredPosition = dropZoneTransform.anchoredPosition;
            }

            Debug.Log("Correct Match!");

            // Play correct sound
            if (soundManager != null)
            {
                soundManager.PlaySpecificSound(correctCandySound);
            }

            // Destroy the matched object
            Destroy(droppedObject);

            // Increment the correct shapes counter
            correctShapesCount++;

            // Check if all shapes are correctly placed
            if (correctShapesCount == totalShapes)
            {
                ClosePanel();
                Debug.Log("All shapes matched! Closing panel.");
            }
        }
        else
        {
            Debug.Log("Incorrect Match!");

            // Play incorrect sound
            if (soundManager != null)
            {
                soundManager.PlaySpecificSound(incorrectCandySound);
            }
        }
    }

    public void ClosePanel()
    {
        if (shapesPanel != null)
        {
            // Add candies to the inventory
            if (candyCollection != null)
            {
                candyCollection.CollectCandy();
                candyCollection.CollectCandy();
            }

            // Close the panel and reset the counter
            shapesPanel.SetActive(false);
            correctShapesCount = 0;
        }
        else
        {
            Debug.LogError("ShapesPanel is not assigned!");
        }
    }
}