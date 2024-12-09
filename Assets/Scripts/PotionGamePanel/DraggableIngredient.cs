using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableIngredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [SerializeField] private string ingredientName; // Set in the Inspector
    [SerializeField] private RecipeManager recipeManager;

    private Vector2 originalPosition; // Store the original position of the ingredient

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // Allow drop detection
        originalPosition = rectTransform.anchoredPosition; // Save the current position
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Drag the object
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Re-enable raycasting

        // Check if dropped in the cauldron
        if (RectTransformUtility.RectangleContainsScreenPoint(
            recipeManager.GetCauldronArea().GetComponent<RectTransform>(),
            Input.mousePosition,
            canvas.worldCamera))
        {
            bool isCorrect = recipeManager.AddIngredient(ingredientName); // Check if the ingredient is correct
            if (isCorrect)
            {
                Destroy(gameObject); // Destroy the ingredient if it's correct
            }
            else
            {
                // If the ingredient is incorrect, return to the original position
                rectTransform.anchoredPosition = originalPosition;
            }
        }
        else
        {
            // If not dropped in the cauldron, return to the original position
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}