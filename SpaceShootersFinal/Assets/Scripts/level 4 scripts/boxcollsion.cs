using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxcollision : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 20; // Specify the data type for the damage variable

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.Instance.Damage(damage); // Corrected method name to Damage
        }
    }
}
