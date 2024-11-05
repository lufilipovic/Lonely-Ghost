using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    private GameObject player; // Reference to the player GameObject
    public float interactionRange = 2f; // Range within which the player can interact with the object
    public GameObject interactableObject; // The interactable object
    public GameObject interactionPanel; // Reference to the UI Panel for interaction

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
        interactionPanel.SetActive(false); // Ensure the interaction panel is initially inactive
    }

    private void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            if (interactionPanel.activeSelf)
            {
                ClosePanel(); // Close the interaction panel if it's already open
            }
            else if (IsPlayerInRange())
            {
                Debug.Log("Hello, I am " + interactableObject.name); // Log interaction with the object
                interactionPanel.SetActive(true); // Show the interaction panel if the player is in range
            }
        }
    }

    // Check if the player is within interaction range of the object
    private bool IsPlayerInRange()
    {
        // Calculate the distance between the object and the player
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= interactionRange; // Return true if the player is within interaction range
    }

    // Method to close the interaction panel
    public void ClosePanel()
    {
        interactionPanel.SetActive(false); // Hide the interaction panel
    }
}
