using UnityEngine;
using TMPro;

public class CandyCollection : MonoBehaviour
{
    public int candyCount = 0; // Counter for collected candy
    public TextMeshProUGUI candyCountText; // Reference to the TextMeshPro component

    // Method to collect candy and increment count
    public void CollectCandy()
    {
        candyCount++; // Increment candy count
        UpdateCandyCountDisplay(); // Update the UI text to show the new candy count
    }

    // Update the UI text with the current candy count
    private void UpdateCandyCountDisplay()
    {
        candyCountText.text = "Candy Count: " + candyCount.ToString(); // Update the text
    }
}
