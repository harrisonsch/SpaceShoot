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
    private AudioSource audioSource; // Audio source component for playing sound effects

        void Start()
        {
                audioSource = gameObject.GetComponent<AudioSource>();
        }
    // Method called to inflict damage on the enemy
    public IEnumerator Damage(float value)
    {
        if(isDying) yield break;
        health -= value; // Decrease enemy's health by the specified value
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


        // Check if enemy is killed
        if (health <= 0 && !isDying)
        {
                isDying = true; // Prevent further execution if dying
                Debug.Log("booming");
                audioSource.Play();
                Debug.Log("waiting");
                Destroy(gameObject);
                SceneManager.LoadScene("WinScene");
        }
        yield return null;
    }
}
