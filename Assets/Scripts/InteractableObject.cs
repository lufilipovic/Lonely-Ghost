using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    private GameObject player; // Reference to the player GameObject
    public float interactionRange = 2f; // Range within which the player can interact with the object
    private UIManager uiManager; // Reference to the UIManager
    private CandyCollection candyCollection; // Reference to the CandyCollection script


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
                //Debug.Log($"{gameObject.name} clicked! Showing panel."); // Log when the object is clicked
                //uiManager.ShowPanel($"Hello, I am {gameObject.name}"); // Show the interaction panel if the player is in range

                // Check if the object has the "Candy" tag
                if (gameObject.CompareTag("Candy"))
                {
                    candyCollection.CollectCandy(); // Collect candy
                    Destroy(gameObject); // Destroy the candy object after collection
                    Debug.Log($"Candy collected! Total candy: {candyCollection.candyCount}"); // Log total candy count
                }
                else
                {
                    Debug.Log($"{gameObject.name} is not candy."); // Log if it's not candy
                    uiManager.ShowPanel("NPCPanel"); // Show the interaction panel if the player is in range
                }
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
        Debug.Log($"Distance to player: {distance}"); // Log the distance to debug
        return distance <= interactionRange; // Return true if the player is within interaction range
    }
}
