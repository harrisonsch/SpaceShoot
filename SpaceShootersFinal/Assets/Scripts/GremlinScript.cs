using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GremlinScript : MonoBehaviour
{
    public float speed = 10f;
    public float arcHeight = 10f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    public bool damageable;
    public TextMeshProUGUI gremlinText;
    public float health = 100f;
    public float startHealth;
    public HealthBar healthBar;
    public Vector3 indicatorSize = new Vector3(3,3,3);
    public GameObject damageText;
    public Transform spawnPos;
    public AudioSource scream;


    public void EjectToCenter(Vector3 center)
    {
        scream.Play();
        startHealth = health;
        startPosition = transform.position;
        targetPosition = center;
        healthBar.SetHealth(health, startHealth);
        StartCoroutine(ArcMoveToCenter());
    }

    IEnumerator GremlinText() {
        gremlinText.enabled = true;
        gremlinText.gameObject.SetActive(true);
        yield return new WaitForSeconds (10f);
        gremlinText.gameObject.SetActive(false);
    }

    IEnumerator ArcMoveToCenter()
    {
        Debug.Log("arc moving");
        float elapsed = 0f;
        float distance = Vector3.Distance(startPosition, targetPosition);
        Vector3 direction = (targetPosition - startPosition).normalized;

        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            elapsed += Time.deltaTime;
            float x = elapsed * speed;
            float y = arcHeight * Mathf.Sin(Mathf.PI * x / distance);
            transform.position = startPosition + direction * x + Vector3.up * y;

            if (x >= distance)
                break;

            yield return null;
        }
        OnReachedCenter();
    }

    void OnReachedCenter()
    {
        Debug.Log("reached center");
        damageable = true;
        StartCoroutine(GremlinText());
    }

    public void Damage(float value)
    {
        if(damageable) {

        
        health -= value;
        if (healthBar != null)
        {
                healthBar.SetHealth(health, startHealth); 
        }
        Debug.Log("hit " + gameObject.name +  " for " + value + " health is now " + health);
        // Vector3 temp = new Vector3(60f, 0, 0);
        DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(value);
        indicator.transform.localScale = indicatorSize;

        if(health <= 0) {
            Debug.Log("killed");
            SceneManager.LoadScene("WinScene");
            Destroy(gameObject);
        }
    }
    }
}