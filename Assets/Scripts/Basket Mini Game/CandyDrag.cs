using UnityEngine;
using UnityEngine.EventSystems;

public class CandyDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    public AudioClip pickupSoundClip; // Reference to this candy's sound

    private ItemPickupSound soundManager; // Centralized sound manager

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;

        // Find the centralized sound manager
        soundManager = FindObjectOfType<ItemPickupSound>();

        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup component is missing from " + gameObject.name + ". Dragging may not work as intended.");
        }
        if (soundManager == null)
        {
            Debug.LogError("Centralized ItemPickupSound manager is missing!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false; // Allow raycasts to pass through
        }

        // Play the pickup sound using the centralized manager
        if (soundManager != null && pickupSoundClip != null)
        {
            soundManager.PlaySpecificSound(pickupSoundClip);
        }

        // Move to the top of the hierarchy
        transform.SetParent(transform.root, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / CanvasScaleFactor();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true; // Enable raycast blocking again
        }

        // Reset position if not dropped correctly
        rectTransform.anchoredPosition = originalPosition;

        // Return to the original parent
        transform.SetParent(originalParent, true);
    }

    private float CanvasScaleFactor()
    {
        return GetComponentInParent<Canvas>().scaleFactor;
    }
}