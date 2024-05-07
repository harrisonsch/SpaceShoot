using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tank", menuName = "PowerUp/Other/Tank")]
public class Tank : PowerUp
{
         private float oldHP = 100f;
    public Tank()
    {
        powerUpName = "Tank";
        duration = 999f; 
        cost = 3f;
        description = "Triples your maximum health";
        value = 20f;
    }

    public override void Activate()
    {
        oldHP = GameController.Instance.maxHealth;
        GameController.Instance.maxHealth = oldHP * 3;
        GameController.Instance.health *= 3;
    }

    public override void Deactivate()
    {
        GameController.Instance.maxHealth = oldHP;
    }
}