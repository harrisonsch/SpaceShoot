using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speedinit = 100f;
    public float damage = 20f;
    public float lifetime = 10f;
    public float speed = 0f;
    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after 'lifetime' seconds
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speedinit * Time.deltaTime);
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Player") {
                GameController gc = GameController.Instance;
                if(gc != null) {
                        Debug.Log("damaging");
                        gc.Damage(damage);
                }
                Destroy(gameObject);
        }
        
    }
}