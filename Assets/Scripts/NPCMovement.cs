using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the NPC movement
    public float patrolDistance = 5f; // Distance the NPC will patrol

    private Vector3 startingPosition; // The starting position of the NPC
    private Vector3 targetPosition; // The current target position for the NPC
    private bool movingTowardsTarget = true; // Direction of movement

    [SerializeField] private int facingDirection = -1;

    private void Start()
    {
        startingPosition = transform.position; // Record the starting position
        targetPosition = startingPosition + new Vector3(patrolDistance, 0, 0); // Set initial target position
    }

    private void Update()
    {
        // Move the NPC towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the NPC has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Switch the target position
            if (movingTowardsTarget)
            {
                targetPosition = startingPosition; // Move back to the starting position
                Flip();
            }
            else
            {
                targetPosition = startingPosition + new Vector3(patrolDistance, 0, 0); // Move to the patrol distance
                Flip();
            }
            movingTowardsTarget = !movingTowardsTarget; // Toggle direction
        }
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
    }
}
