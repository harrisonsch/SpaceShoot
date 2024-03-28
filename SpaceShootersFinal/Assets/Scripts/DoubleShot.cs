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
        value = 1f;
        description = "Shoots +" + (int) value + " bullets";
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