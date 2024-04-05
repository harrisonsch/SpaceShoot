using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Bosstwo : MonoBehaviour
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
    public bool moveAway = false;
    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        healthText.text = "Boss: " + health.ToString();
        if(shotCooldown < 0f) {
            Debug.Log("shooting");
            StartCoroutine(ShootSpiralPattern3D());
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

   


IEnumerator ShootSpiralPattern3D() {
    float horizontalAngle = 0f;
    float verticalAngle = 0f;
    float angleStep = 2f;
    float verticalAngleStep = 5f;
    float speed = 500f;
    float acceleration = 20f;
    int bulletsPerWave = 100;
    float timeBetweenBullets = 0.05f;
    bool increasing = true;

    Quaternion initialRotation = Quaternion.LookRotation(transform.forward, Vector3.up);

    for (int i = 0; i < bulletsPerWave; i++) {
        float bulletDirXPosition = Mathf.Sin((horizontalAngle * Mathf.PI) / 180);
        float bulletDirYPosition = Mathf.Sin((verticalAngle * Mathf.PI) / 180);
        float bulletDirZPosition = Mathf.Cos((horizontalAngle * Mathf.PI) / 180);
        Vector3 localBulletDirection = new Vector3(bulletDirXPosition, bulletDirYPosition, bulletDirZPosition);

        // Apply the initial rotation to the bullet direction to ensure it's correctly oriented.
        Vector3 worldBulletDirection = initialRotation * localBulletDirection.normalized;

        Quaternion bulletRotation = Quaternion.LookRotation(worldBulletDirection, Vector3.up);
        GameObject bulletObj = Instantiate(bullet, spawnPos.position + worldBulletDirection, bulletRotation);
        BossBullet bossBullet = bulletObj.GetComponent<BossBullet>();

        if (bossBullet != null) {
            bossBullet.speedinit = speed;
            speed += acceleration;
        }

        horizontalAngle += angleStep;

        if (increasing) {
            verticalAngle += verticalAngleStep;
            if (verticalAngle > 25) {
                increasing = false;
            }
        } else {
            verticalAngle -= verticalAngleStep;
            if (verticalAngle < -25) {
                increasing = true;
            }
        }

        yield return new WaitForSeconds(timeBetweenBullets);
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
                audioSource.Play();
            Destroy(gameObject);
            SceneManager.LoadScene("WinScene");
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


void ShootRadialBurst() {
    int bulletCount = 36;
    float angleStep = 360f / bulletCount;
    float angle = 0f;
    System.Random rand = new System.Random();
    for (int i = 0; i < bulletCount; i++) {
        float upwardBias = (i % 6 == 0) ? rand.Next(-1, 2) * 0.5f : 0.1f;
        float bulletDirXPosition = Mathf.Sin((angle * Mathf.PI) / 180);
        float bulletDirZPosition = Mathf.Cos((angle * Mathf.PI) / 180);
        Vector3 bulletDirection = new Vector3(bulletDirXPosition, upwardBias, bulletDirZPosition).normalized;
        Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection);
        GameObject bulletObj = Instantiate(bullet, spawnPos.position, bulletRotation);
        BossBullet bossBullet = bulletObj.GetComponent<BossBullet>();
        if (bossBullet != null) {
            bossBullet.speedinit = bulletSpeed;
            bossBullet.damage = bulletDamage;
            bossBullet.lifetime = lifetime;
        }
        angle += angleStep;
    }
}

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
    FlyingController playerController = player.GetComponent<FlyingController>();

    
    Vector3 forwardVelocity = player.transform.forward * playerController.moveSpeed;
    Vector3 rightVelocity = player.transform.right * playerController.moveSpeed;
    Vector3 upVelocity = player.transform.up * playerController.moveSpeed;

    Vector3 combinedVelocity = (forwardVelocity + rightVelocity + upVelocity);

    return combinedVelocity;
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
