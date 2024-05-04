using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DestroyableWall : MonoBehaviour
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
        indicator.transform.localScale = new Vector3(3, 3, 3); 

        if (health <= 0 && !isDying)
        {
            isDying = true;
            boom.Play();
            gameObject.SetActive(false);
            Destroy(gameObject, boom.clip.length);
            died = true;
        }
    }
}







