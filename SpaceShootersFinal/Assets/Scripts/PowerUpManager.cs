using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    
    public static PowerUpManager Instance { get; private set; }
    public List<PowerUp> powerUpRegister;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (PowerUp powerUp in powerUpRegister) {
                RegisterPowerUp(powerUp);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private List<PowerUp> powerUps = new List<PowerUp>();

    public void RegisterPowerUp(PowerUp powerUp)
    {
        if (!powerUps.Contains(powerUp))
        {
            powerUps.Add(powerUp);
        }
    }

    public T GetPowerUp<T>() where T : PowerUp
    {
        foreach (var powerUp in powerUps)
        {
            if (powerUp is T)
            {
                return powerUp as T;
            }
        }
        return null;
    }
}