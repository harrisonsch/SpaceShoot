using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameController : MonoBehaviour
{
        public int maxHealth = 100;
        public int currHealth = 100;
        public bool fired = false;
        public float gunShotRange = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealthAdjustment(int healthBonus) {
        currHealth += healthBonus;
        maxHealth += healthBonus;
    }
    public void PlayerGunFire()
    {
        if (fired == true)
        {
        //     GameObject gunShotInstance = Instantiate(ShotPrefab, gun.position, gun.rotation);
        //     gunShotInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, gunShotRange);
 
            //Debug.Log("Fired playerSpeed: " + playerSpeed);
            //Debug.Log("Fired gunShotRange: " + gunShotRange);
            //Debug.Log("Fired throttle.value: " + gunShotInstance.GetComponent<Rigidbody>().velocity.z);
            //Debug.Log("Fired gunshotRB speed: " + playerScript.playerSpeed);
 
        //     Destroy(gunShotInstance, 3f);
        }
    }
 
    public void FireButton(bool _fired)
    {
        fired = _fired;
    }
}

// public class HealthBonus: MonoBehaviour {

//     [SerializeField]
//     private float bonusAmount;
    
//     void Start() {
//         setHealthAdjustment(bonusAmount);
//     }

// //     private void OnDestroy() {
// //         maxHealth -= bonusAmount;
// //     }
// }