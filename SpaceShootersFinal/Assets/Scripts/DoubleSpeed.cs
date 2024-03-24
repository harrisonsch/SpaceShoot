using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DoubleSpeed", menuName = "PowerUp/Speed/DoubleSpeed")]
public class DoubleSpeed: PowerUp
{
//     bool isActive = false;
    public DoubleSpeed()
    {
        powerUpName = "DoubleSpeed";
        duration = 999f; // lasts for 10 seconds
        cost = 5f;
        description = "Doubles your speed";
    }
    public override void Activate()
    {
        // if(isActive == false) {
            GameController.Instance.currSpeedMult *= 2;
        //     isActive = true;
        // }
    }

    public override void Deactivate()
    {
        // if(isActive) {
        //     isActive = false;
            GameController.Instance.currSpeedMult /= 2;
        // }
    }
}