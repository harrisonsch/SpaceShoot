// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FlyingController : MonoBehaviour
// {
//     public GameController gameController;

//     public float moveSpeed = 10f; 
//     public float turnSpeed = 60f; 
//     public float rotationSpeed = 5.0f;

//     public float maxYAngle = 80f;
//     public float minYAngle = -80f;
//     public float deadzoneRadius = 50f;
//     public float sensitivity = 5f;


//       public bool pressingThrottle = false;
//     public bool throttle => pressingThrottle;

//     public float pitchPower, rollPower, yawPower, enginePower, turnPower;
//     public float powerMult = 1f;

//     private float activeRoll, activePitch, activeYaw, activeTurn;


//         private Vector3 lastCollisionDirection = Vector3.zero;
//     private bool isInReversal = false;
//     private float reversalDuration = 1.0f; 
//     private float reversalTimer = 0.0f;

//     private Camera mainCamera;

//     private Vector2 currentRotation;

//     private void Start()
//     {  
//         gameController = FindObjectOfType<GameController>();
//         mainCamera = FindObjectOfType<Camera>(); 
//         currentRotation = new Vector2(transform.localEulerAngles.y, -transform.localEulerAngles.x);
//     }
   

//     private void Update()
//     {
//         if (gameController != null)
//         {
//             moveSpeed = GameController.Instance.GetEnginePower();
//         }


//         // Cursor.lockState = CursorLockMode.Locked;
//        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
//         float distanceFromCenter = Vector2.Distance(Input.mousePosition, screenCenter);

//         if (distanceFromCenter > deadzoneRadius)
//         {
            
//             Vector2 mouseDisplacement = new Vector2(Input.mousePosition.x - screenCenter.x, Input.mousePosition.y - screenCenter.y);

        
//             currentRotation.x += mouseDisplacement.x * sensitivity * Time.deltaTime;
//             currentRotation.y += mouseDisplacement.y * sensitivity * Time.deltaTime;

//             currentRotation.y = Mathf.Clamp(currentRotation.y, minYAngle, maxYAngle);

//             Quaternion xQuaternion = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
//             Quaternion yQuaternion = Quaternion.AngleAxis(currentRotation.y, Vector3.left);

//             transform.localRotation = xQuaternion * yQuaternion;
//         }


//         // forward and back
//         float vertical = Input.GetAxis("Horizontal");
//         transform.Translate(Vector3.up * vertical * moveSpeed * Time.deltaTime, Space.Self);

//         // Movement up and down
//         float horizontal = Input.GetAxis("Vertical");
//         float leftright = Input.GetAxis("Turn");
//         transform.Translate(Vector3.right * leftright * moveSpeed * Time.deltaTime, Space.Self);
//         //left and right
        
//         transform.Translate(Vector3.forward * horizontal * moveSpeed * Time.deltaTime, Space.Self);
//     }

// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    public GameController gameController;
public Transform shipVisual; // Assign this in the inspector
    public float moveSpeed = 10f; 
    public float turnSpeed = 60f; 
    public float rotationSpeed = 5.0f;

    public float maxYAngle = 80f;
    public float minYAngle = -80f;
    public float deadzoneRadius = 50f;
    public float sensitivity = 5f;

    public float maxRollAngle = 30f;

    public bool pressingThrottle = false;
    public bool throttle => pressingThrottle;

    public float pitchPower, rollPower, yawPower, enginePower, turnPower;
    public float powerMult = 1f;

    private Rigidbody rb; 

    private Camera mainCamera;

    private Vector2 currentRotation;

    private float currentRoll = 0f;

    private void Start()
    {  
        gameController = FindObjectOfType<GameController>();
        mainCamera = FindObjectOfType<Camera>(); 
        currentRotation = new Vector2(transform.localEulerAngles.y, -transform.localEulerAngles.x);
        rb = GetComponent<Rigidbody>(); 
    }
   

    private void FixedUpdate() 
    {
        if (gameController != null)
        {
            moveSpeed = GameController.Instance.GetEnginePower();
        }

        HandleMouseLook();
        HandleMovement();
    }

    private void HandleMouseLook()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        float distanceFromCenter = Vector2.Distance(Input.mousePosition, screenCenter);

        if (distanceFromCenter > deadzoneRadius)
        {
            Vector2 mouseDisplacement = new Vector2(Input.mousePosition.x - screenCenter.x, Input.mousePosition.y - screenCenter.y);

            currentRotation.x += mouseDisplacement.x * sensitivity * Time.fixedDeltaTime;
            currentRotation.y += mouseDisplacement.y * sensitivity * Time.fixedDeltaTime;
            currentRotation.y = Mathf.Clamp(currentRotation.y, minYAngle, maxYAngle);

            Quaternion xQuaternion = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(currentRotation.y, Vector3.left);
            rb.MoveRotation(xQuaternion * yQuaternion); 
        } 
        else {
                rb.angularVelocity = Vector3.zero;
        }
    }
      

private void HandleMovement()
{
    Vector3 movement = new Vector3(Input.GetAxis("Turn") * moveSpeed, Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
    rb.velocity = transform.TransformDirection(movement);

    float targetRoll = Input.GetAxis("Turn") * maxRollAngle;
    currentRoll = Mathf.Lerp(currentRoll, targetRoll, Time.fixedDeltaTime * 5f);

    shipVisual.localRotation = Quaternion.Euler(0, 0, -currentRoll);
}


    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Wall") {
                Debug.Log("entered");
        }
    }
}