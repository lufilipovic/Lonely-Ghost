using UnityEngine;
using TMPro;

public class CandyCollection : MonoBehaviour
{
    public int candyCount = 0; // Counter for collected candy
    public int friendCount = 0; // Counter for collected candy
    public TextMeshProUGUI candyCountText; // Reference to the TextMeshPro component
    public TextMeshProUGUI friendCountText;

    // Method to collect candy and increment count
    public void CollectCandy()
    {
        candyCount++; // Increment candy count
        UpdateCandyCountDisplay(); // Update the UI text to show the new candy count
    }

    public void MakeFriend()
    {
        friendCount++;
        UpdateFriendCountDisplay();
        RemoveCandy(5);
        UpdateCandyCountDisplay();
    }

    // Update the UI text with the current candy count
    private void UpdateCandyCountDisplay()
    {
        candyCountText.text = candyCount.ToString(); // Update the text
        Debug.Log("Candy count:" + candyCount.ToString());
    }

    private void UpdateFriendCountDisplay()
    {
        friendCountText.text = friendCount.ToString(); // Update the text
        Debug.Log("Friend count:" + friendCount.ToString());
    }

    public void RemoveCandy(int amount)
    {
        if (candyCount >= amount)
        {
            candyCount -= amount;
            Debug.Log($"Candies Removed: {amount}. Total: {candyCount}");
            UpdateCandyCountDisplay();
        }
        else
        {
            Debug.LogWarning("Not enough candies to remove!");
        }
    }
}
