using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private GameObject player; // Reference to the player GameObject
    public float interactionRange = 2f; // Range within which the player can interact with the object
    private UIManager uiManager; // Reference to the UIManager
    public string panelToShow; // Public variable to set which panel to show

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
        uiManager = FindObjectOfType<UIManager>(); // Find the UIManager in the scene
    }

    private void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            // Check if the player is within interaction range of this object
            if (IsPlayerInRange())
            {
                Debug.Log($"{gameObject.name} clicked! Showing panel."); // Log when the object is clicked
                uiManager.ShowPanel(panelToShow); // Show the specified interaction panel
            }
            //else
            //{
            //    Debug.Log($"{gameObject.name} is out of range."); // Log if the player is out of range
            //}
        }
    }

    // Check if the player is within interaction range of the object
    private bool IsPlayerInRange()
    {
        // Calculate the distance between the object and the player
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log($"Distance to player: {distance}"); // Log the distance to debug
        return distance <= interactionRange; // Return true if the player is within interaction range
    }
}
