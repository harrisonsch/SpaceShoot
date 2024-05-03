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
    public bool moveAway = false;
    public AudioSource audioSource;
    public float accuracy = 0.5f;
    public bool playerLook = false;
    public int burstCount = 1;
    public float burstCD = 0.5f;
    public Vector3 indicatorSize = new Vector3(3,3,3);
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
        if(playerLook) {

        LookAtPlayer();
        }
        if(healthText != null) {

        healthText.text = "Boss: " + health.ToString();
        }
        if(shotCooldown < 0f) {
            Debug.Log("shooting");
        //     ShootRadialBurst();
        //     StartCoroutine(ShootSpiralPattern3D()); 
        StartCoroutine(ShootBurst(burstCount, burstCD));
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

    void FrankShoot()
    {
        
    }

    IEnumerator ShootBurst(int burstCount, float delayBetweenShots) {
    if (playerPos != null) {
        for (int i = 0; i < burstCount; i++) {
            Vector3 directionToPlayer = playerPos.position - transform.position;
            directionToPlayer = AccuracyAdjuster(directionToPlayer, accuracy);
            Quaternion bulletRotation = Quaternion.LookRotation(directionToPlayer);
            GameObject bulletObj = Instantiate(bullet, transform.position, bulletRotation);
            BossBullet bossbullet = bulletObj.GetComponent<BossBullet>();
            if (bossbullet != null) {
                bossbullet.speedinit = bulletSpeed;
                bossbullet.damage = bulletDamage;
                bossbullet.lifetime = lifetime;
            }
            yield return new WaitForSeconds(delayBetweenShots);
        }
    }
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
        indicator.transform.localScale = indicatorSize;
        if(healthText != null) 
        {

        healthText.text = "Boss: " + health.ToString();
        }

        if(health <= 0) {
            Debug.Log("killed");
            if(audioSource != null) {
                audioSource.Play();
            }
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
        
        Vector3 directionToPlayer = playerPos.position - transform.position;
     
        directionToPlayer = AccuracyAdjuster(directionToPlayer, accuracy);
        
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
