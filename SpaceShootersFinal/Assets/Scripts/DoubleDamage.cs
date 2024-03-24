using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DoubleDamage", menuName = "PowerUp/Damage/DoubleDamage")]
public class DoubleDamage : PowerUp
{
    bool isActive = false;
    public DoubleDamage()
    {
        powerUpName = "DoubleDamage";
        duration = 999f; // lasts for 10 seconds
        cost = 5f;
        description = "Doubles your damage";
        value = 2;
    }

    public override void Activate()
    {
        if(isActive == false) {
            GameController.Instance.currDamageMult *= value;
            isActive = true;
        }
    }

    public override void Deactivate()
    {
        if(isActive) {
            isActive = false;
            GameController.Instance.currDamageMult /= value;
        }
    }
}