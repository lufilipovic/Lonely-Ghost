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

    }

}
