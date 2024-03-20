using UnityEngine;

public class Bullet : MonoBehaviour
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
        speed = GameController.Instance.GetEnginePower() * 4 + speedinit;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("entered");
        if(other.gameObject.tag == "enemy") {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if(enemy != null) {
                        enemy.Damage(damage);
                }
                Destroy(gameObject);
        }
        
        // Add collision handling here (e.g., damage enemy, destroy bullet)
    }
}