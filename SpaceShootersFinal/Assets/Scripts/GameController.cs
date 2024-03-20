using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public List<PowerUp> activePowerUps = new List<PowerUp>();
    public float baseDamage = 20f;
    public float baseSpeed = 100f;
    public float currSpeedMult = 1;
    public float currSpeedAdds = 0;
    public float currDamageMult = 1;
    public float currDamageAdds= 0;
    public float health = 100f;
    float currDamage;
    float currSpeed;
    public GameObject player;
    private PowerUpManager powerUpManager;
    public TextMeshProUGUI healthText;
    
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
        healthText.text = "Health: " + health.ToString();
        CalculateCurrentStats();
        
    }

    void CalculateCurrentStats()
    {

        // Apply active power-ups
        foreach (var powerUp in activePowerUps)
        {
        //     powerUp.Activate(player);
        }
        currSpeed = (baseSpeed + currSpeedAdds) * currSpeedMult;
        currDamage = (baseDamage + currDamageAdds) * currDamageMult;
        // Update player's stats if needed. This could be done within each powerUp's Activate/Deactivate methods.
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        powerUp.Activate();
        if (!activePowerUps.Contains(powerUp))
        {
            activePowerUps.Add(powerUp);
        }
    }

    public float GetEnginePower()
    {
        return currSpeed;
    }

    public void SetEnginePower(float value)
    {
        currSpeed = value;
        // Here you can add any validation or additional logic
        // For example, triggering events or notifications when the value changes
    }
    
    public void Damage(float value)
    {
        
        health -= value;
        Debug.Log("Ship hit for " + value + " health is now " + health);
        if(health <= 0){
                Debug.Log("Game Over");
                Application.Quit();
        }
    }
}