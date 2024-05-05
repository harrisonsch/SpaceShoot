using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shield", menuName = "PowerUp/Other/Shield")]
public class Shield : PowerUp
{
         private float oldHP = 100f;
    public Shield()
    {
        powerUpName = "Shield";
        duration = 999f; 
        cost = 4f;
        description = "Adds 100 HP";
        value = 100f;
    }

    public override void Activate()
    {
        oldHP = GameController.Instance.maxHealth;
        GameController.Instance.maxHealth = oldHP + (int)value;
        GameController.Instance.health += oldHP + (int)value;
    }

    public override void Deactivate()
    {
        GameController.Instance.maxHealth = GameController.Instance.maxHealth - (int)value;
    }
}