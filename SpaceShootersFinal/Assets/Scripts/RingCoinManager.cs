using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCoinManager : MonoBehaviour
{
    public GameObject ringCoinPrefab; 
    public Transform plane;
    public int coinGain = 15;
    public float spawnHeight = 50f; // Height above plane to spawn the ring-coin

    private GameObject currentRingCoin;

    void Update()
    {
        // Check GameController's health
        if (GameController.Instance.health < 90 && currentRingCoin == null)
        {
            // Spawn a ring-coin if health is low and no ring-coin is active
            if (plane != null)
            {
                // Calculate spawn position above plane
                Vector3 spawnPosition = plane.position + Vector3.up * spawnHeight;
                
                // Rotate by 90 degrees around the x-axis
                Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);

                // Instantiate ring-coin at calculated position and rotation
                Debug.Log("Spawning ring-coin at position: " + spawnPosition);
                currentRingCoin = Instantiate(ringCoinPrefab, spawnPosition, spawnRotation);
            }
            else
            {
                Debug.LogError("Plane object is not assigned!");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Update balance and destroy ring-coin when collision occurs
            GameController.Instance.balance += coinGain;
            Debug.Log("Ring-coin collected. Balance: " + GameController.Instance.balance);
            Destroy(currentRingCoin);
        }
    }
}
