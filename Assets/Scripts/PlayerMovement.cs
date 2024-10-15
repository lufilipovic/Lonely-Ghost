using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f; // Movement speed of the player

    private Rigidbody2D rb; // Rigidbody component for physics interactions
    private Vector2 movementDirection; // Direction of player movement

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Freeze rotation of the Rigidbody
    }

    private void Update()
    {
        // Read player input for movement direction
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed; // Apply movement to the Rigidbody

        // Check for collisions with screen edges
        CheckScreenEdgeCollisions();
    }

    // Method to check for collisions with screen edges and apply knockback
    void CheckScreenEdgeCollisions()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position); // Convert world position to screen coordinates
        float screenWidth = Screen.width; // Get the width of the screen
        float screenHeight = Screen.height; // Get the height of the screen

        // Check left edge collision
        if (screenPosition.x <= 0)
        {
            Vector2 knockbackDirection = Vector2.right;
            StartCoroutine(AnimateKnockback(knockbackDirection)); // Apply knockback towards right
        }
        // Check right edge collision
        else if (screenPosition.x >= screenWidth)
        {
            Vector2 knockbackDirection = Vector2.left;
            StartCoroutine(AnimateKnockback(knockbackDirection)); // Apply knockback towards left

        }
        // Check bottom edge collision
        if (screenPosition.y <= 0)
        {
            Vector2 knockbackDirection = Vector2.up;
            StartCoroutine(AnimateKnockback(knockbackDirection)); // Apply knockback upwards

        }
        // Check top edge collision
        else if (screenPosition.y >= screenHeight)
        {
            Vector2 knockbackDirection = Vector2.down;
            StartCoroutine(AnimateKnockback(knockbackDirection)); // Apply knockback downwards

        }
    }

    // Coroutine to animate knockback effect
    IEnumerator AnimateKnockback(Vector2 direction)
    {
        rb.velocity = Vector2.zero; // Stop player movement

        // Wait for a short duration
        yield return new WaitForSeconds(0.2f);

        // Resume player movement in the specified direction
        rb.velocity = movementDirection * movementSpeed;
    }

    //// Triggered when player collides with a trigger collider
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    var dialogScript = other.gameObject.GetComponent<DialogueSystem>(); // Get DialogueSystem component from the collider
    //    if (dialogScript)
    //    {
    //        dialogScript.StartDialog(); // Start dialogue with the collided object
    //    }
    //}

}
