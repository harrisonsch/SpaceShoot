using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoubleSpeed : PowerUp
{
    bool isActive = false;
    void Awake()
    {
        PowerUpManager.Instance.RegisterPowerUp(this);
    }
    public DoubleSpeed()
    {
        powerUpName = "DoubleSpeed";
        duration = 999f; // lasts for 10 seconds
    }

    public override void Activate(GameObject player)
    {
        if(isActive == false) {
            GameController.Instance.currSpeedMult *= 2;
            isActive = true;
        }
    }

    public override void Deactivate(GameObject player)
    {
        if(isActive) {
            isActive = false;
            GameController.Instance.currSpeedMult /= 2;
        }
    }
}