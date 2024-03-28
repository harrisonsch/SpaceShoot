using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DoubleShot", menuName = "PowerUp/Bullets/DoubleShot")]
public class DoubleShot : PowerUp
{
    public DoubleShot()
    {
        powerUpName = "DoubleShot";
        duration = 999f; 
        cost = 5f;
        description = "Shoots two bullets at once";
        value = 1f;
    }

    public override void Activate()
    {
        GameController.Instance.shots += value;
    }

    public override void Deactivate()
    {
        GameController.Instance.shots -= value;
    }
}