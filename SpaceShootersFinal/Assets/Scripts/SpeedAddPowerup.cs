using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpeedAddPowerup", menuName = "PowerUp/Speed/SpeedAddPowerup")]
public class SpeedAddPowerup : PowerUp
{
    bool isActive = false;
    public SpeedAddPowerup()
    {
        powerUpName = "SpeedAddPowerup";
        duration = 999f; 
        cost = 5f;
        description = "increase the speed";
        value = 20f;
    }

    public override void Activate()
    {
        GameController.Instance.currSpeedAdds += value;
    }

    public override void Deactivate()
    {
        GameController.Instance.currSpeedAdds -= value;
    }
}