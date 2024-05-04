using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GlassCannon", menuName = "PowerUp/GlassCannon")]
public class GlassCannon : PowerUp
{
        private float oldHP = 100f;
    public GlassCannon()
    {
        powerUpName = "Glass Cannon";
        duration = 999f; 
        cost = 5f;
        description = "Doubles your damage, halves your max and current HP";
        value = 20f;
    }


    public override void Activate()
    {
        oldHP = GameController.Instance.maxHealth;
        GameController.Instance.health = GameController.Instance.health / 2;
        GameController.Instance.maxHealth = GameController.Instance.maxHealth / 2;
        GameController.Instance.currDamageMult = GameController.Instance.currDamageMult * 2;
    }

    public override void Deactivate()
    {
        GameController.Instance.maxHealth = oldHP;
        GameController.Instance.currDamageMult =  GameController.Instance.currDamageMult / 2;
    }
}