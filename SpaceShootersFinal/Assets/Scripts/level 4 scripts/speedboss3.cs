using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedboss3 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    private GameController gameController; // Specify the type for 'speed'

    private void Start()
    {
        gameController = GameController.Instance;
    }

    private void FixedUpdate()
    {
        float newSpeed = gameController.GetEnginePower() * speed; // Calculate new speed
        
        Vector3 movement = Vector3.up * newSpeed * Time.deltaTime; // Calculate movement
        
        transform.Translate(movement); // Apply movement to the object's position
    }

}
