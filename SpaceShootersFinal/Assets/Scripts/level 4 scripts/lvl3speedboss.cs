using UnityEngine;

public class lvl3speedboss : MonoBehaviour
{
    public float baseSpeed = 1.5f;
    public float speed;
    private GameController gameController;
    public Transform player;

    public float closeThreshold = 10.0f; 
    public float farThreshold = 50.0f;    

    public float farSpeedMult = 0.5f;
    public float closeSpeedMult = 1.15f;

    private void Start()
    {
        gameController = GameController.Instance;
    }

    private void FixedUpdate()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > farThreshold)
        {
                Debug.Log("ship is far and moving slower");
            speed = farSpeedMult; 
        }
        else if (distanceToPlayer < closeThreshold)
        {
                Debug.Log("ship is close and moving faster");
            speed = closeSpeedMult; 
        } else {
                speed = baseSpeed;
        }

        float newSpeed = gameController.GetEnginePower() * speed;

        Vector3 movement = Vector3.up * newSpeed;
        transform.Translate(movement * Time.deltaTime);
    }
}
