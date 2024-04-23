using UnityEngine;
using System.Collections;

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
        damage = GameController.Instance.GetDamage();
        speed = GameController.Instance.GetEnginePower() * 4 + speedinit;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("entered");
        if(other.gameObject.tag == "enemy") {
                BossEnemy enemy = other.gameObject.GetComponent<BossEnemy>();
                if(enemy != null) {
                        enemy.Damage(damage);
                } else {
                        EnemyMonster monster = other.gameObject.GetComponent<EnemyMonster>();
                        if(monster != null) {
                                monster.Damage(damage);
                        }
                }
                Destroy(gameObject);
        } else if (other.gameObject.tag == "critPoint") {
                CritPoint critPoint = other.gameObject.GetComponent<CritPoint>();
                critPoint.takeDamage(damage);
                Destroy(gameObject);
        } else if (other.gameObject.tag == "TinyShip") {
                TinyShipHandler tinyShip = other.gameObject.GetComponent<TinyShipHandler>();
                tinyShip.Damage(damage);
                Destroy(gameObject);
        }
        
    }
}