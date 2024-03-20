using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpCollider : MonoBehaviour
{
    public PowerUp powerup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player") 
        {
                Destroy(gameObject);
            GameController.Instance.ActivatePowerUp(powerup);
        }
    }
}
