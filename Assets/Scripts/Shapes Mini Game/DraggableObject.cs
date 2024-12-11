using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 originalPosition; // To reset position if needed

    public AudioClip pickupSoundClip; // Reference to the drag sound

    private ItemPickupSound soundManager; // Reference to the centralized sound manager

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Ensure the CanvasGroup exists and is configured properly
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Find the centralized sound manager
        soundManager = FindObjectOfType<ItemPickupSound>();
        if (soundManager == null)
        {
            Debug.LogError("Centralized ItemPickupSound manager is missing!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Play the pickup sound using the centralized manager
        if (soundManager != null && pickupSoundClip != null)
        {
            soundManager.PlaySpecificSound(pickupSoundClip);
        }

        originalPosition = rectTransform.anchoredPosition; // Store the original position
        canvasGroup.blocksRaycasts = false; // Allow drop detection
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Drag the object
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Re-enable raycasting

        // Check if dropped outside any valid drop zone
        if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<DropZone>() == null)
        {
            rectTransform.anchoredPosition = originalPosition; // Reset position
        }
    }
}