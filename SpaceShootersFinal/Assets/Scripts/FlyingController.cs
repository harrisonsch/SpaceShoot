using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    public GameController gameController;

    public float moveSpeed = 10f; // Speed of the plane movement
    public float turnSpeed = 60f; // Speed of turning the plane
    public float rotationSpeed = 5.0f;

      public bool pressingThrottle = false;
    public bool throttle => pressingThrottle;

    public float pitchPower, rollPower, yawPower, enginePower, turnPower;
    public float powerMult = 1f;

    private float activeRoll, activePitch, activeYaw, activeTurn;

    private Camera mainCamera;

    private void Start()
    {  
        gameController = FindObjectOfType<GameController>();
        mainCamera = FindObjectOfType<Camera>(); 
    }

    private void Update()
    {
        if (gameController != null)
        {
            // Adjust moveSpeed or turnSpeed based on your game's logic
        }

        // Cursor.lockState = CursorLockMode.Locked;
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            // Debug line to visualize the ray in the Scene view
        //     Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            // Determine the target rotation to look at the point of intersection
            Quaternion targetRotation = Quaternion.LookRotation(pointToLook - transform.position);

            // Adjust the target rotation to only rotate around the Y axis
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            // Smoothly rotate the plane towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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