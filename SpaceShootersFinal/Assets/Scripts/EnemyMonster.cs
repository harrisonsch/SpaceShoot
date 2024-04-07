using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyMonster : MonoBehaviour
{
    public float health = 200f; // Current health of the enemy
    public float startHealth; // Initial health of the enemy
    public TextMeshProUGUI healthText; // Reference to text UI element to display health
    public HealthBar healthBar; // Reference to a health bar component
    public GameObject damageText; // Prefab for a damage text indicator
    public Transform spawnPos; // Position where damage text indicator will spawn

    private AudioSource audioSource; // Audio source component for playing sound effects

    // Method called to inflict damage on the enemy
    public void Damage(float value)
    {
        health -= value; // Decrease enemy's health by the specified value

        // Update health bar if available
        if (healthBar != null)
        {
            healthBar.SetHealth(health, startHealth); 
        }

        // Display damage indicator
        DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(value);
        indicator.transform.localScale = new Vector3(3, 3, 3); 

        // Update health text to display current health
        healthText.text = "Boss: " + health.ToString();

        // Check if enemy is killed
        if (health <= 0)
        {
            audioSource.Play(); // Play audio cue for enemy death
            Destroy(gameObject); // Destroy enemy GameObject
            SceneManager.LoadScene("WinScene"); // Load win scene
        }
    }
}
