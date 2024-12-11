using TMPro;
using UnityEngine;

public class CandyPickUp : MonoBehaviour
{
    public float pickupRange = 2f; // Range within which the player can pick up the candy
    private GameObject player; // Reference to the player
    public AudioClip pickupSoundClip; // Reference to this candy's sound

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' is missing!");
        }

        if (pickupSoundClip == null)
        {
            Debug.LogWarning("Pickup sound clip is not assigned to this candy!");
        }
    }

    private void Update()
    {
        if (IsPlayerInRange() && Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            CandyCollection candyCollector = player.GetComponent<CandyCollection>();

            if (candyCollector != null)
            {
                // Use a centralized AudioManager for playing the pickup sound
                ItemPickupSound centralizedSoundManager = FindObjectOfType<ItemPickupSound>();
                if (centralizedSoundManager != null && pickupSoundClip != null)
                {
                    centralizedSoundManager.PlaySpecificSound(pickupSoundClip);
                }

                candyCollector.CollectCandy(); // Increment the candy count in CandyCollector
                Destroy(gameObject); // Remove the candy from the scene
            }
            else
            {
                Debug.LogError("CandyCollection script is missing on the Player GameObject!");
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