using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldestroy : MonoBehaviour
{
    // Start is called before the first frame update
     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the GameObject this script is attached to
        }
    }
    
}
