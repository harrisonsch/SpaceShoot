using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalTinyShip : MonoBehaviour
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
    public float minOrbitDistance = 10f;
    public float maxOrbitDistance = 20f;
    public bool shooting = true;
    public bool orbit = true;
    public bool spawner = true;

    public float strafeSpeed = 1f;
public float strafeRange = 1f;
        public float strafeSmoothness = 5f;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(spawner) {

                RandomizePosition();
        }
        if(orbit) {

        orbitDistance = Random.Range(minOrbitDistance, maxOrbitDistance);
        }
        shootCooldown = shootRate;
    }

    

        void Update()
        {       
                if(orbit) {

                OrbitPlayer();
                }
                if(shooting) {

                ShootAtPlayer();
                }
        }

    void RandomizePosition()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * orbitDistance;
        transform.position = player.position + offset;
    }

        public void OrbitPlayer()
        {
                // Calculate the horizontal movement offset within a range
                float strafeOffset = Mathf.Sin(Time.time * strafeSpeed) * strafeRange;

                // Calculate the target position based on the player's position and the strafe offset
                Vector3 targetPosition = player.position + player.right * strafeOffset;

                // Smoothly move towards the target position
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * strafeSmoothness);
        }

    void ShootAtPlayer()
    {
        shootCooldown -= Time.deltaTime;
        if (shootCooldown <= 0)
        {
            Vector3 shootDirection = (player.position - transform.position).normalized;
            Quaternion shootRotation = Quaternion.LookRotation(shootDirection);
            Instantiate(bulletPrefab, transform.position, shootRotation);  
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
                if(spawner) {

                GameObject.FindGameObjectWithTag("TinyShipSpawner").GetComponent<EnemySpawner>().shipDied();
                }
                GameObject plane = transform.Find("tiny_ship").gameObject; 
                if (plane != null) {
                        plane.SetActive(false);
                }
                Destroy(gameObject, boom.clip.length);
        }
    }
    
}