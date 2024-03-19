using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoubleDamage : PowerUp
{
    bool isActive = false;
    void Awake()
    {
        PowerUpManager.Instance.RegisterPowerUp(this);
    }
    public DoubleDamage()
    {
        powerUpName = "DoubleDamage";
        duration = 999f; // lasts for 10 seconds
    }

    public override void Activate(GameObject player)
    {
        if(isActive == false) {
            GameController.Instance.currDamageMult *= 2;
            isActive = true;
        }
    }

    public override void Deactivate(GameObject player)
    {
        if(isActive) {
            isActive = false;
            GameController.Instance.currDamageMult /= 2;
        }
    }
}