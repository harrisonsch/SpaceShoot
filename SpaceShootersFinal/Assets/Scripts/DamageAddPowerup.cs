using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DamageAddPowerup", menuName = "PowerUp/Damage/DamageAddPowerup")]
public class DamageAddPowerup : PowerUp
{
    bool isActive = false;
    public DamageAddPowerup()
    {
        powerUpName = "INSERTNAME";
        duration = 999f; 
        cost = 5f;
        description = "ADD DESC HERE";
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