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
    // public float accuracy = 0.8f;
    private float shotCooldown = 2f;
    public float health = 200f;
    public float startHealth;
    public TextMeshProUGUI healthText;
    public GameObject player;
    public HealthBar healthBar;
    public GameObject damageText;
    public Transform spawnPos;
    public float strafeRange = 10f;
    public float strafeSpeed = 5f;
    private Vector3 strafeTargetPosition;
    private Vector3 initialRightDirection;
    private Vector3 initialPosition;
    private bool movingRight = true;
    public float strafeDelay = 0.5f; 
    private float strafeTimer;
    public float moveAwaySpeed = 75f;
    private Vector3 strafeDirection;
    private float directionTimer = 0;
    public float changeDirectionTime = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startHealth = health;
        initialPosition = transform.position;
        initialRightDirection = transform.right;
        strafeTargetPosition = transform.position + (initialRightDirection * strafeRange);
        strafeTimer = strafeDelay;
        strafeDirection = Random.value > 0.5f ? transform.right : -transform.right;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<FlyingController>().throttle == true) {
            MoveBackwardsAndStrafe();
        } else {
            Strafe();
        }
        LookAtPlayer();
        healthText.text = "Boss: " + health.ToString();
        if(shotCooldown < 0f) {
            Debug.Log("shooting");
            ShootAtPlayer();
            shotCooldown = shootingRate;
        }
        
        shotCooldown -= Time.deltaTime;

        if (directionTimer <= 0)
        {
            strafeDirection = -strafeDirection;
            directionTimer = changeDirectionTime;
        }
        else
        {
            directionTimer -= Time.deltaTime;
        }
    }

    void MoveBackwardsAndStrafe()
    {
        Vector3 retreatDirection = (transform.position - player.transform.position).normalized;
        Vector3 movePosition = transform.position + retreatDirection * moveAwaySpeed * Time.deltaTime;
        movePosition += strafeDirection * strafeSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, movePosition, (strafeSpeed + moveAwaySpeed) * Time.deltaTime);
    }

    void MoveBackwards() {
        // Calculate the direction from the boss to the player
        Vector3 directionToPlayer = player.transform.position - transform.position;

        // Move the boss away from the player
        // The speed of moving back could be influenced by the player's engine power, for example
        Vector3 moveBackPosition = transform.position - directionToPlayer.normalized * (moveAwaySpeed + player.GetComponent<FlyingController>().enginePower * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, moveBackPosition, moveAwaySpeed * Time.deltaTime);
    }

    void LookAtPlayer()
    {
        if (playerPos != null)
        {
                Vector3 directionToPlayer = playerPos.position - transform.position;
                directionToPlayer.y = 0;
        
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                
                // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                transform.rotation = lookRotation;
        }
    }

    // void Strafe()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, strafeTargetPosition, strafeSpeed * Time.deltaTime);

    //     if (Vector3.Distance(transform.position, strafeTargetPosition) < 0.1f)
    //     {
    //         if (strafeTargetPosition.x > transform.position.x)
    //         {
    //             strafeTargetPosition = transform.position - (transform.right * strafeRange);
    //         }
    //         else
    //         {
    //             strafeTargetPosition = transform.position + (transform.right * strafeRange);
    //         }
    //     }
    // }

    void Strafe()
    {
        // Calculate target based on initial position and direction
        Vector3 targetPosition = initialPosition + (movingRight ? transform.right : -transform.right) * strafeRange;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, strafeSpeed * Time.deltaTime);

        // Check if close to the target position to switch direction
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight; // Switch direction
            // Update the initial position to current position to make movement continuous
            initialPosition = transform.position;
        }
    }

    public void Damage(float value)
    {
        
        health -= value;
        if (healthBar != null)
        {
                healthBar.SetHealth(health, startHealth); 
        }
        Debug.Log("hit " + gameObject.name +  " for " + value + " health is now " + health);
        // Vector3 temp = new Vector3(60f, 0, 0);
        DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(value);
        indicator.transform.localScale = new Vector3(3,3,3); 
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
        Vector3 estimatedPlayerVelocity = CalculateEstimatedVelocity();
        
        Vector3 directionToPlayer = playerPos.position - transform.position;
        
        float bulletTravelTime = directionToPlayer.magnitude / bulletSpeed;

        Vector3 predictedPlayerPosition = playerPos.position + estimatedPlayerVelocity * bulletTravelTime;

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
