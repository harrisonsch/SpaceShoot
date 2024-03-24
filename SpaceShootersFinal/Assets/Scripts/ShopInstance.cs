using UnityEngine;
using System.Collections.Generic;

public class ShopInstance : MonoBehaviour
{
    private PowerUpManager powerUpManager;
    private List<PowerUp> availablePowerUps;
    public int shopItemsCount = 2;
    public List<PowerUp> currentShop = new List<PowerUp>(); 

    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        InitializeShop();
    }

    void InitializeShop()
    {
        availablePowerUps = powerUpManager.powerUpRegister;
        for (int i = 0; i < shopItemsCount && availablePowerUps.Count > 0; i++)
        {
            int randIndex = Random.Range(0, availablePowerUps.Count);
            currentShop.Add(availablePowerUps[randIndex]);
            availablePowerUps.RemoveAt(randIndex); // Prevents the same power-up from being added twice
        }
    }

    public void Purchase(PowerUp powerUp)
    {
        // Example purchase method, assumes you have a way to check player's currency
        if (PlayerHasEnoughCurrency(powerUp.cost))
        {
            ApplyPowerUp(powerUp);
            currentShop.Remove(powerUp);
        }
        else
        {
            Debug.Log("Not enough currency to purchase this power-up.");
        }
    }

    private bool PlayerHasEnoughCurrency(float cost)
    {
        // if bal >= cost
        // Implement check against player's currency here
        return true; // Placeholder
    }

    private void ApplyPowerUp(PowerUp powerUp)
    {
        // Assuming you have a method to apply the power-up effect
        GameController.Instance.ActivatePowerUp(powerUp);
    }
}