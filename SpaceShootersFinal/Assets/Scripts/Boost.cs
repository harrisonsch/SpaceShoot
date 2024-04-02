using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Boost", menuName = "PowerUp/Speed/Boost")]
public class Boost : PowerUp
{
    public Boost()
    {
        powerUpName = "Boost";
        duration = 999f; 
        cost = 5f;
        description = "Every 10 seconds, press SHIFT to boost";
        value = 20f;
    }


    public override void Activate()
    {
        GameController.Instance.boostActive = true;
    }

    public override void Deactivate()
    {
        GameController.Instance.boostActive = false;
    }
}