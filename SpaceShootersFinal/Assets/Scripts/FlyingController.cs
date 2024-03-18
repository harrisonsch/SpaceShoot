using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    public bool pressingThrottle = false;
    public bool throttle => pressingThrottle;
    private GameController gameController;

    public float pitchPower, rollPower, yawPower, enginePower, turnPower;

    private float activeRoll, activePitch, activeYaw, activeTurn;
    private void Start()
    {  
        gameController = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        if(gameController != null)
        {
            enginePower = gameController.GetEnginePower();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pressingThrottle == false)
            {

                pressingThrottle = true;

            }
            else if (pressingThrottle == true)
            {

                pressingThrottle = false;

            }
        }
        if (throttle)
        {
            transform.position += transform.forward * enginePower * Time.deltaTime;

            activePitch = Input.GetAxisRaw("Vertical") * pitchPower * Time.deltaTime;
            activeRoll = Input.GetAxisRaw("Horizontal") * rollPower * Time.deltaTime;
            activeYaw = Input.GetAxisRaw("Yaw") * yawPower * Time.deltaTime;
            activeTurn = Input.GetAxisRaw("Turn") * turnPower * Time.deltaTime;

            transform.Rotate((activePitch * pitchPower * Time.deltaTime),
                (activeYaw * yawPower * Time.deltaTime),
                -activeRoll * rollPower * Time.deltaTime,
                Space.Self);

                transform.Rotate(Vector3.up * activeTurn);
        }
        else
        {
            activePitch = Input.GetAxisRaw("Vertical") * (pitchPower/2) * Time.deltaTime;
            activeRoll = Input.GetAxisRaw("Horizontal") * (rollPower/2) * Time.deltaTime;
            activeYaw = Input.GetAxisRaw("Yaw") * (yawPower/2) * Time.deltaTime;
            activeTurn = Input.GetAxisRaw("Turn") * (turnPower/2) * Time.deltaTime;

            transform.Rotate((activePitch * pitchPower * Time.deltaTime),
                activeYaw * yawPower * Time.deltaTime,
                -activeRoll * rollPower * Time.deltaTime,
                Space.Self);
        }
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("collided");
    }
}