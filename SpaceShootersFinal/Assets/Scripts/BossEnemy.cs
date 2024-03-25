using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour
{
    public Transform playerPos;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDamage;
    public float lifetime;
    public float shootingRate = 2f;
    public float accuracy = 0.8f;
    private float shotCooldown = 2f;
    public float health = 200f;
    public float startHealth;
    public TextMeshProUGUI healthText;
    public GameObject player;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startHealth = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Boss: " + health.ToString();
        if(shotCooldown < 0f) {
            Debug.Log("shooting");
            ShootAtPlayer();
            shotCooldown = shootingRate;
        }
        
        shotCooldown -= Time.deltaTime;
    }

    public void Damage(float value)
    {
        
        health -= value;
        if (healthBar != null)
        {
                healthBar.SetHealth(health, startHealth); 
        }
        Debug.Log("hit " + gameObject.name +  " for " + value + " health is now " + health);
        healthText.text = "Boss: " + health.ToString();
        if(health <= 0) {
            Debug.Log("killed");
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
    }
    

    // void ShootAtPlayer() {
    //     if(playerPos != null) {
    //         Vector3 directionToPlayer = playerPos.position - transform.position;
    //         directionToPlayer = AccuracyAdjuster(directionToPlayer, accuracy);
    //         Quaternion bulletRotation = Quaternion.LookRotation(directionToPlayer);
    //         GameObject bulletObj = Instantiate(bullet, transform.position, bulletRotation); 
    //         BossBullet bossbullet = bulletObj.GetComponent<BossBullet>();

    //         if(bossbullet != null) {
    //             bossbullet.speedinit = bulletSpeed;
    //             bossbullet.damage = bulletDamage;
    //             bossbullet.lifetime = lifetime;
    //         }
    //     }
    // }

    void ShootAtPlayer() {
    if(playerPos != null) {
        // Estimate the player's velocity
        Vector3 estimatedPlayerVelocity = CalculateEstimatedVelocity();
        
        Vector3 directionToPlayer = playerPos.position - transform.position;
        
        // Calculate bullet travel time to current player position
        float bulletTravelTime = directionToPlayer.magnitude / bulletSpeed;

        // Predict player's future position
        Vector3 predictedPlayerPosition = playerPos.position + estimatedPlayerVelocity * bulletTravelTime;

        // Aim at predicted position
        directionToPlayer = predictedPlayerPosition - transform.position;
        Quaternion bulletRotation = Quaternion.LookRotation(directionToPlayer);

        GameObject bulletObj = Instantiate(bullet, transform.position, bulletRotation);
        BossBullet bossbullet = bulletObj.GetComponent<BossBullet>();

        if(bossbullet != null) {
            bossbullet.speedinit = bulletSpeed;
            bossbullet.damage = bulletDamage;
            bossbullet.lifetime = lifetime;
        }
    }
}

Vector3 CalculateEstimatedVelocity() {
    // Assuming your player's forward speed is the primary component of their movement,
    // and they move primarily in the direction they are facing.
    float enginePower = player.GetComponent<FlyingController>().enginePower;
    Vector3 movementDirection = player.transform.forward;
    
    // Adjust this calculation if the player can move significantly in other directions.
    return movementDirection * enginePower;
}

    Vector3 AccuracyAdjuster(Vector3 originalDirection, float accuracy)
    {
        if (accuracy < 1f)
        {
            float inaccuracy = 1f - accuracy;
            float horizontalDeviationAngle = Random.Range(-inaccuracy * 45f, inaccuracy * 45f);
            float verticalDeviationAngle = Random.Range(-inaccuracy * 45f, inaccuracy * 45f);
            
            Quaternion deviationRotation = Quaternion.Euler(verticalDeviationAngle, horizontalDeviationAngle, 0);
            return deviationRotation * originalDirection;
        }
        return originalDirection;
    }
}
