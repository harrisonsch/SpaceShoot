using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_move :  MonoBehaviour
{
    public Transform playerShip; // Reference to the player ship
    public float maxSpeed = 10f; // Maximum speed of the enemy ship
    public float minSpeed = 2f; // Minimum speed of the enemy ship
    public float followDistance = 10f; // Distance at which the player ship starts following
    public float acceleration = 5f; // Rate at which the enemy ship accelerates/decelerates

    private float currentSpeed; // Current speed of the enemy ship

    private void Start()
    {
        currentSpeed = maxSpeed;
    }

    private void Update()
    {
        // Check if playerShip reference is assigned
        if (playerShip != null)
        {
            // Calculate the distance between the enemy ship and the player ship
            float distanceToPlayer = Vector3.Distance(transform.position, playerShip.position);

            // If the player ship is within follow distance, move the player ship towards the enemy ship
            if (distanceToPlayer >= followDistance)
            {
                // Accelerate towards maximum speed
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);

                // Calculate the direction from the player ship to the enemy ship
                Vector3 direction = transform.position - playerShip.position;

                // Normalize the direction vector to get a unit direction vector
                direction.Normalize();

                // Move the enemy ship forward
                transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
            }
            else
            {
                // Slow down towards minimum speed
                currentSpeed = Mathf.MoveTowards(currentSpeed, minSpeed, acceleration * Time.deltaTime);

                // Move the enemy ship forward at the current speed
                transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
            }
        }
    }
}
