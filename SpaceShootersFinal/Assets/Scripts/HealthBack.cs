using UnityEngine;
using UnityEngine.UI;

public class HealthBack : MonoBehaviour
{
    public Transform camTransform;

   



     void LateUpdate()
    {
        transform.LookAt(transform.position + camTransform.forward);
    }

}