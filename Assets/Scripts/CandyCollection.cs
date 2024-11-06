using UnityEngine;

public class CandyCollection : MonoBehaviour
{
    public int candyCount = 0; // Counter for collected candy

    // Method to collect candy and increment count
    public void CollectCandy()
    {
        candyCount++; // Increment candy count
        Debug.Log("Candy collected! Total candy: " + candyCount); // Log the count
    }
}
