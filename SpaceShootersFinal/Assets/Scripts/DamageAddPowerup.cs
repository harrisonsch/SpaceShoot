using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DamageAddPowerup", menuName = "PowerUp/Damage/DamageAddPowerup")]
public class DamageAddPowerup : PowerUp
{
    bool isActive = false;
    public DamageAddPowerup()
    {
        powerUpName = "DamageAddPowerup";
        duration = 999f; 
        cost = 5f;
        description = "Increase the damage of the plane";
        value = 20f;
    }

    public override void Activate()
    {
        GameController.Instance.currDamageAdds += value;
    }

    public override void Deactivate()
    {
        GameController.Instance.currDamageAdds -= value;
    }
}