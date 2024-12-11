using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private string matchingTag; // Set this in the inspector for each zone
    public GameObject shapesPanel;
    public int totalShapes; // Total number of shapes to match (set this in the inspector)

    private static int correctShapesCount = 0; // Shared counter for correct shapes placed
    private CandyCollection candyCollection; // Reference to the CandyCollection script

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
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null && droppedObject.CompareTag(matchingTag))
        {
            // Snap the object to the drop zone
            droppedObject.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("Correct Match!");
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
        }
    }

    public void ClosePanel()
    {
        if (shapesPanel != null)
        {
            candyCollection.CollectCandy();
            shapesPanel.SetActive(false);
            correctShapesCount = 0; // Reset counter for future use

        }
        else
        {
            Debug.LogError("MiniGamePanel is not assigned!");
        }
        //shapesPanel.SetActive(false);
        //correctShapesCount = 0; // Reset counter for future use
    }
}