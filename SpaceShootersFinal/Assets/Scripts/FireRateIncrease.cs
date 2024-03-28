using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FireRateIncrease", menuName = "PowerUp/Bullets/FireRateIncrease")]
public class FireRateIncrease : PowerUp
{
    public FireRateIncrease()
    {
        powerUpName = "INSERTNAME";
        duration = 999f; 
        cost = 5f;
        description = "ADD DESC HERE";
        value = 1f;
    }

    public override void Activate()
    {
        GameController.Instance.fireRate += value;
    }

    public override void Deactivate()
    {
        GameController.Instance.fireRate -= value;
    }
}