using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyShipHandler : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public float orbitDistance = 15f;
    public float orbitSpeed = 5f;
    public float shootRate = 2f;
    private float shootCooldown;
    public float health = 10f;
    public AudioSource boom;
    public GameObject damageText;
    public Transform spawnPos;
    private bool isDying = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Initialize position
        RandomizePosition();
        shootCooldown = shootRate;
    }

    void Update()
    {
        OrbitPlayer();
        ShootAtPlayer();
    }

    void RandomizePosition()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * orbitDistance;
        transform.position = player.position + offset;
    }

//     void OrbitPlayer()
//     {
//         transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);
//         Vector3 toPlayer = transform.position - player.position;
//         transform.position = player.position + toPlayer.normalized * orbitDistance;  // Maintain distance
//     }
        void OrbitPlayer()
{
    // Erratic movement modification
    float randomOffset = Random.Range(-0.5f, 0.5f); // Random offset for more unpredictable movement

    transform.RotateAround(player.position, Vector3.up, (orbitSpeed + randomOffset) * Time.deltaTime);

    Vector3 toPlayer = transform.position - player.position;
    transform.position = player.position + toPlayer.normalized * orbitDistance;
}

    void ShootAtPlayer()
    {
        shootCooldown -= Time.deltaTime;
        if (shootCooldown <= 0)
        {
            Vector3 shootDirection = (player.position - transform.position).normalized;
            Quaternion shootRotation = Quaternion.LookRotation(shootDirection);
            Instantiate(bulletPrefab, transform.position, shootRotation);  // Shoot directly at player
            shootCooldown = shootRate;
        }
    }
    public void Damage(float value)
    {
        
        health -= value;
        Debug.Log("hit " + gameObject.name +  " for " + value + " health is now " + health);
        DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(value);
        indicator.transform.localScale = Vector3.zero;
        if(health <= 0 && !isDying) {
        isDying = true;
            Debug.Log("killed");
                boom.Play();
                GameObject plane = transform.Find("plane").gameObject; // Adjust "Plane" to the actual name of your child GameObject
                if (plane != null) {
                        plane.SetActive(false);
                }
                Destroy(gameObject, boom.clip.length);
        }
    }
}