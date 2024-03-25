using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    public bool pressingThrottle = false;
    public bool throttle => pressingThrottle;
    private GameController gameController;

    public float pitchPower, rollPower, yawPower, enginePower, turnPower;
    public float powerMult = 1f;

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
            powerMult = gameController.currSpeedMult;
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
            transform.position += transform.forward * (enginePower) * Time.deltaTime;

            activePitch = Input.GetAxisRaw("Vertical") * (pitchPower * powerMult) * Time.deltaTime;
            activeRoll = Input.GetAxisRaw("Horizontal") * (rollPower * powerMult) * Time.deltaTime;
            activeYaw = Input.GetAxisRaw("Yaw") * (yawPower * powerMult) * Time.deltaTime;
            activeTurn = Input.GetAxisRaw("Turn") * (turnPower * powerMult) * Time.deltaTime;

            transform.Rotate(activePitch,
                activeYaw,
                -activeRoll,
                Space.Self);

                transform.Rotate(Vector3.up * activeTurn);
        }
        else
        { 
            activePitch = Input.GetAxisRaw("Vertical") * ((pitchPower  * powerMult)/2) * Time.deltaTime;
            activeRoll = Input.GetAxisRaw("Horizontal") * ((rollPower * powerMult)/2) * Time.deltaTime;
            activeYaw = Input.GetAxisRaw("Yaw") * ((yawPower * powerMult)/2) * Time.deltaTime;
            activeTurn = Input.GetAxisRaw("Turn") * ((turnPower * powerMult)/2) * Time.deltaTime;

            transform.Rotate(activePitch, activeYaw, -activeRoll,
                Space.Self);
            transform.Rotate(Vector3.up * activeTurn);    
        }
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("collided");
    }
}