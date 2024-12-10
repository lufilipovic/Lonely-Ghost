using UnityEngine;
using UnityEngine.EventSystems;

public class CandyDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;

        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup component is missing from " + gameObject.name + ". Dragging may not work as intended.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false; // Allow raycasts to pass through
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