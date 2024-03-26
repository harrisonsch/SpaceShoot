using UnityEngine;

public class HealthBarPositioner : MonoBehaviour
{
    public Transform boss; 
    public Vector3 offset = new Vector3(0, 1.0f, 0); 

    void Update()
    {
        if (boss != null)
        {
            transform.position = boss.position + offset;

          
            transform.LookAt(Camera.main.transform.position);
            //transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        }
    }
}