using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Boost", menuName = "PowerUp/Speed/Boost")]
public class Boost : PowerUp
{
        public bool isActive;
        private float nextBoost = 0f;
    public Boost()
    {
        powerUpName = "Boost";
        duration = 999f; 
        cost = 5f;
        description = "Every x seconds, press SHIFT to boost";
        value = 20f;
    }

    void Update() {
        if(isActive) {
               if (Input.GetKey(KeyCode.LeftShift) && Time.time >= nextBoost) {
                        GameController.Instance.baseSpeed *= 10;
                        //this should happen for like 0.1 seconds, then turn off and disable until nextFireTime which should be like 10 seconds
                        nextBoost = Time.time + 1f / 5f;
               }
        }
    }

    public override void Activate()
    {
        isActive = true;
        GameController.Instance.currDamageAdds += value;
    }

    public override void Deactivate()
    {
        isActive = false;
        GameController.Instance.currDamageAdds -= value;
    }
}