using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyMonster : MonoBehaviour
{
    public float health = 200f; // Current health of the enemy
    public float startHealth; // Initial health of the enemy
    public TextMeshProUGUI healthText; // Reference to text UI element to display health
    public GameObject damageText; // Prefab for a damage text indicator
    public Transform spawnPos; // Position where damage text indicator will spawn
    private bool isDying = false;
    public AudioSource boom; // Audio source component for playing sound effects
    private bool died = false;
    private Rigidbody rb;
    public bool boss = true;
    public string sceneToLoad = "LevelOneTransition";
    public int bounty = 10;
    private bool paid = false;
    
    public HealthBar healthBar;

    private void Start()
    {
        startHealth = health;
        rb = GetComponent<Rigidbody>();
        // healthBar.SetHealth(health, startHealth);
    }

    public void Damage(float value)
    {
        health -= value;
       if (healthBar != null)
        {
                healthBar.SetHealth(health, startHealth); 
        }

        // Display damage indicator
        DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(value);
        indicator.transform.localScale = new Vector3(1, 1, 1); 

        if (health <= 0 && !isDying)
        {
            isDying = true;
            boom.Play();
            gameObject.SetActive(false);
            Destroy(gameObject, boom.clip.length);
            died = true;
        }
        if (died && boss && !paid)
        {
                paid = true;
                GameController.Instance.balance += bounty;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}








// using UnityEngine;
// using TMPro;
// using UnityEngine.SceneManagement;
// using System.Collections;
// using UnityEngine.UI;

// public class EnemyMonster : MonoBehaviour
// {
//     public float health = 200f; // Current health of the enemy
//     public float startHealth = health; // Initial health of the enemy
//     public TextMeshProUGUI healthText; // Reference to text UI element to display health
//     public GameObject damageText; // Prefab for a damage text indicator
//     public Transform spawnPos; // Position where damage text indicator will spawn
//     private bool isDying = false;
//     public AudioSource boom; // Audio source component for playing sound effects
//     private bool died = false;
//     Rigidbody rb;
//     [SerializeField] FloatingHealthBar healthBar;
//     private void Start()
//     {
//         health = startHealth;

//     }
//     private void Awake()
//     {
//         rb =GetComponent<Rigidbody>();
//         healthBar = GetComponentInChildren<FloatingHealthBar>();
//         healthBar.updateHealthBar(health,startHealth);
//     }
//     public void Damage(float value)
//     {
//         health -= value;
//         Debug.Log(health);
//         healthBar.updateHealthBar(health,startHealth);
//         // Update health bar if available
       

//         // Display damage indicator
//         DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
//         indicator.SetDamageText(value);
//         indicator.transform.localScale = new Vector3(3, 3, 3); 

//         if(health <= 0 && !isDying) {
//         isDying = true;
//             Debug.Log("killed");
//                 boom.Play();
//                 GameObject plane = transform.gameObject; 
//                 if (plane != null) {
//                         plane.SetActive(false);
//                 }
//                 Destroy(gameObject, boom.clip.length);
//                 died = true;
//         }
//         if(died) {
//                 SceneManager.LoadScene("LevelOneTransition");  
//         }
//     }
// }
