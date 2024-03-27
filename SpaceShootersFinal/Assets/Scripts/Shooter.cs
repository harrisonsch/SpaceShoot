using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 5f; 
    private Vector3 aim;
    public Camera cam;
    public Vector3 mousePos;
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
            Debug.Log(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.transform.LookAt(hit.point);
            }
            // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
       
    }
}