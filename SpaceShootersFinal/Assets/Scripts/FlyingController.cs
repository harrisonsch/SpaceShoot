using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    public GameController gameController;

    public float moveSpeed = 10f; // Speed of the plane movement
    public float turnSpeed = 60f; // Speed of turning the plane
    public float rotationSpeed = 5.0f;

    public float maxYAngle = 80f;
    public float minYAngle = -80f;
    public float deadzoneRadius = 50f;
    public float sensitivity = 5f;

      public bool pressingThrottle = false;
    public bool throttle => pressingThrottle;

    public float pitchPower, rollPower, yawPower, enginePower, turnPower;
    public float powerMult = 1f;

    private float activeRoll, activePitch, activeYaw, activeTurn;

    private Camera mainCamera;

    private Vector2 currentRotation;

    private void Start()
    {  
        gameController = FindObjectOfType<GameController>();
        mainCamera = FindObjectOfType<Camera>(); 
        currentRotation = new Vector2(transform.localEulerAngles.y, -transform.localEulerAngles.x);
    }
   

    private void Update()
    {
        if (gameController != null)
        {
            // Adjust moveSpeed or turnSpeed based on your game's logic
        }

        // Cursor.lockState = CursorLockMode.Locked;
       Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        float distanceFromCenter = Vector2.Distance(Input.mousePosition, screenCenter);

        if (distanceFromCenter > deadzoneRadius)
        {
            // Calculate mouse displacement from the center
            Vector2 mouseDisplacement = new Vector2(Input.mousePosition.x - screenCenter.x, Input.mousePosition.y - screenCenter.y);

            // Adjust rotation based on mouse position and sensitivity
            // To invert the horizontal movement, change the sign of mouseDisplacement.x in the equation
            // To keep vertical movement non-inverted, ensure the sign for mouseDisplacement.y remains negative
            currentRotation.x += mouseDisplacement.x * sensitivity * Time.deltaTime;
            // If vertical movement feels inverted, adjust the sign here as well
            currentRotation.y += mouseDisplacement.y * sensitivity * Time.deltaTime;

            // Clamp the vertical rotation
            currentRotation.y = Mathf.Clamp(currentRotation.y, minYAngle, maxYAngle);

            // Apply the rotation to the object
            Quaternion xQuaternion = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(currentRotation.y, Vector3.left);

            transform.localRotation = xQuaternion * yQuaternion;
        }


        // forward and back
        float vertical = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.up * vertical * moveSpeed * Time.deltaTime, Space.Self);

        // Movement up and down
        float horizontal = Input.GetAxis("Vertical");
        float leftright = Input.GetAxis("Turn");
        transform.Translate(Vector3.right * leftright * moveSpeed * Time.deltaTime, Space.Self);
        //left and right
        
        transform.Translate(Vector3.forward * horizontal * moveSpeed * Time.deltaTime, Space.Self);
    }

}