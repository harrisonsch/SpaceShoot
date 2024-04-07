using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidscollision : MonoBehaviour
{
    public GameObject targetObject; // The GameObject whose position you want to set
    public Transform newPosition; // The transform whose position will be set

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            newPosition.position = targetObject.transform.position;
        }
    }
}
