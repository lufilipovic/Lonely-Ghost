using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private string matchingTag; // Set this in the inspector for each zone
    public GameObject shapesPanel;
    public int totalShapes; // Total number of shapes to match (set this in the inspector)

    private static int correctShapesCount = 0; // Shared counter for correct shapes placed

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null && droppedObject.CompareTag(matchingTag))
        {
            // Snap the object to the drop zone
            droppedObject.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("Correct Match!");

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
        shapesPanel.SetActive(false);
        correctShapesCount = 0; // Reset counter for future use
    }
}