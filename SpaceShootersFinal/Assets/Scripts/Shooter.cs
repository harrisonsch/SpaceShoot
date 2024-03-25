using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 5f; 

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.X)) 
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
    }
}