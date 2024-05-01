using UnityEngine;
using System.Collections;

public class Level4_move : MonoBehaviour
{
    public float maxSpeed = 10f; // Maximum speed of the enemy ship
    public float minSpeed = 2f; // Minimum speed of the enemy ship
    public float acceleration = 5f; // Rate at which the enemy ship accelerates/decelerates

    private float currentSpeed; // Current speed of the enemy ship

    private void Start()
    {
        currentSpeed = maxSpeed;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            MoveEnemyShip();
            yield return null; // Wait for the next frame
        }
    }

    private void MoveEnemyShip()
    {
        // Move the enemy ship forward
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime*2);
    }
}
