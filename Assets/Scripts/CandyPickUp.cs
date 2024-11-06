using UnityEngine;

public class CandyPickUp : MonoBehaviour
{
    public float pickupRange = 2f; // Range within which the player can pick up the candy
    private GameObject player; // Reference to the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
    }

    private void Update()
    {
        if (IsPlayerInRange() && Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            CandyCollection candyCollector = player.GetComponent<CandyCollection>();
            if (candyCollector != null)
            {
                candyCollector.CollectCandy(); // Increment the candy count in CandyCollector
                Destroy(gameObject); // Remove the candy from the scene
            }
        }
    }

    // Check if the player is within pickup range
    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= pickupRange;
    }
}
