using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float value)
    {
        health -= value;
        Debug.Log("hit " + gameObject.name +  " for " + value + " health is now " + health);
        if(health <= 0) {
            Debug.Log("killed");
            Destroy(gameObject);
        }
    }
}
