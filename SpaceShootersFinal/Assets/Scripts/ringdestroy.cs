using UnityEngine;

public class ringdestroy : MonoBehaviour
{
    public int coinGain = 15;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collision detected. Gaining coins and destroying the ring.");
            
            // Update balance and destroy ring-coin when collision occurs
            GameController.Instance.balance += coinGain;
            Debug.Log("Ring-coin collected. Balance: " + GameController.Instance.balance);
            Destroy(gameObject);
        }
    }
}
