using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public List<PowerUp> activePowerUps = new List<PowerUp>();
    public float baseDamage = 20f;
    public float baseSpeed = 500f;
    public float currSpeedMult = 1;
    public float currSpeedAdds = 0;
    public float currDamageMult = 1;
    public float currDamageAdds= 0;
    public float baseHealth = 100f;
    public float health = 100f;
    public int balance = 30;
    public float fireRate = 5f;
    public float shots = 1f;
    float currDamage;
    float currSpeed;
    public GameObject player;
    private PowerUpManager powerUpManager;
    public TextMeshProUGUI healthText;
    public bool hasMoved = false;
    public bool boostActive = false;
     private float nextBoostTime = 0f; 
    public float boostCooldown = 1f; 
    public float boostDuration = 1f; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currDamage = baseDamage;
        currSpeed = baseSpeed;
        powerUpManager = FindObjectOfType<PowerUpManager>();
        healthText.text = "Health: " + health.ToString();
        // DoubleSpeed doubleSpeed = new DoubleSpeed();
        // powerUpManager.RegisterPowerUp(doubleSpeed);
        // activePowerUps.Add(doubleSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Health") != null) {

                healthText = GameObject.FindGameObjectWithTag("Health").GetComponent<TextMeshProUGUI>();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextBoostTime && boostActive)
        {
            StartCoroutine(ActivateBoost());
            nextBoostTime = Time.time + boostCooldown; // Reset cooldown
        }
        healthText.text = "Health: " + health.ToString();
        CalculateCurrentStats();
        
    }

    IEnumerator ActivateBoost()
    {
        Debug.Log("boosting");
        float originalSpeed = baseSpeed;
        baseSpeed *= 10; // Apply boost effect
        yield return new WaitForSeconds(boostDuration); // Wait for the boost duration
        baseSpeed = originalSpeed; // Revert to original speed
    }

    void CalculateCurrentStats()
    {

        // Apply active power-ups
        foreach (var powerUp in activePowerUps)
        {
        }
        currSpeed = (baseSpeed + currSpeedAdds) * currSpeedMult;
        currDamage = (baseDamage + currDamageAdds) * currDamageMult;
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        powerUp.Activate();
        if (!activePowerUps.Contains(powerUp))
        {
            activePowerUps.Add(powerUp);
        }
    }

    public void DeactivatePowerUp(PowerUp powerUp)
    {
        powerUp.Deactivate();
        if (!powerUpManager.powerUpRegister.Contains(powerUp))
        {
            powerUpManager.powerUpRegister.Add(powerUp);
        }
        activePowerUps.Remove(powerUp);
    }

    public float GetEnginePower()
    {
        return currSpeed;
    }

    public float GetDamage()
    {
        return currDamage;
    }

    public void SetEnginePower(float value)
    {
        currSpeed = value;
    }
    
    public void Damage(float value)
    {
        
        health -= value;
        Debug.Log("Ship hit for " + value + " health is now " + health);
        if(health <= 0){
                Debug.Log("Game Over");
                SceneManager.LoadScene("GameOverScene");
        }
    }
}