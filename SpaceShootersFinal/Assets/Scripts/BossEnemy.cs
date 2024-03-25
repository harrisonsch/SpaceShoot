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
    private float shotCooldown;
    public float health = 200f;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("hit " + gameObject.name +  " for " + value + " health is now " + health);
        healthText.text = "Boss: " + health.ToString();
        if(health <= 0) {
            Debug.Log("killed");
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");
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
            float deviationAngle = Random.Range(-inaccuracy * 45f, inaccuracy * 45f);
            Quaternion deviationRotation = Quaternion.Euler(0, 0, deviationAngle);
            return deviationRotation * originalDirection;
        }
        return originalDirection;
    }
}
