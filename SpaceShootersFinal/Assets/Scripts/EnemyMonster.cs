using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyMonster : MonoBehaviour
{
    public float health = 200f; // Current health of the enemy
    public float startHealth; // Initial health of the enemy
    public TextMeshProUGUI healthText; // Reference to text UI element to display health
    public HealthBar healthBar; // Reference to a health bar component
    public GameObject damageText; // Prefab for a damage text indicator
    public Transform spawnPos; // Position where damage text indicator will spawn
        private bool isDying = false;
    public AudioSource boom; // Audio source component for playing sound effects
    private bool died = false;

        void Start()
        {
        }
    public void Damage(float value)
    {
        health -= value;
        Debug.Log(health);
        // Update health bar if available
        if (healthBar != null)
        {
            healthBar.SetHealth(health, startHealth); 
        }

        // Display damage indicator
        DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(value);
        indicator.transform.localScale = new Vector3(3, 3, 3); 

        if(health <= 0 && !isDying) {
        isDying = true;
            Debug.Log("killed");
                boom.Play();
                GameObject plane = transform.gameObject; 
                if (plane != null) {
                        plane.SetActive(false);
                }
                Destroy(gameObject, boom.clip.length);
                died = true;
        }
        if(died) {
                SceneManager.LoadScene("NewScene1");  
        }
        // Check if enemy is killed
        // if (health <= 0 && !isDying)
        // {
        //         isDying = true; // Prevent further execution if dying
        //         Debug.Log("booming");
        //         booming.Play();
        //         Debug.Log("waiting");
        //         Destroy(gameObject);
        //         SceneManager.LoadScene("NewScene1");
        // }
    }
}
