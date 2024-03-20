using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the default bullet prefab in the inspector
    public Transform bulletSpawnPoint;
    public float fireRate = 5f; // Bullets per second

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.X)) // Use X to shoot, change as needed
        {
            if(Time.time >= nextFireTime) {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
            
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        // You can modify the instantiated bullet here based on active power-ups
    }
}