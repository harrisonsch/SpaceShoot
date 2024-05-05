using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;
    public Transform alternateTarget;
    public Vector3 offset = new Vector3(0, 5, -10);
    public Vector3 alternateOffset = new Vector3(0, 5, -10);
    public float followSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    public bool lowerCameraFocus = false;
    public bool useAlternateTarget = false;

//     private void LateUpdate()
//     {
//         if (followTarget == null) return;

//         Vector3 desiredPosition = followTarget.position + followTarget.TransformDirection(offset);
        
//         transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

//         Quaternion desiredRotation = Quaternion.LookRotation(followTarget.position - transform.position, Vector3.up);

//         transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
//     }
    private void LateUpdate()
    {
        if (followTarget == null) return;
        if(useAlternateTarget) {
                Vector3 desiredPosition = alternateTarget.position + alternateTarget.TransformDirection(alternateOffset);
        
                transform.position = desiredPosition;

                Quaternion desiredRotation = Quaternion.LookRotation(alternateTarget.position - transform.position, Vector3.up);

                transform.rotation = desiredRotation;

        } else {
               

                Vector3 desiredPosition = followTarget.position + followTarget.TransformDirection(offset);
        
                transform.position = desiredPosition;

                Quaternion desiredRotation = Quaternion.LookRotation(followTarget.position - transform.position, Vector3.up);

                transform.rotation = desiredRotation;
        }
        
    }
}
