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
    public GameObject flyingVFX;
    private bool dead = false;
    public GameObject goblinGFX;
    public GameObject boomVFX;
    public AudioSource boomSFX;


    public void EjectToCenter(Vector3 center)
    {
        flyingVFX.SetActive(true);
        scream.Play();
        startHealth = health;
        startPosition = transform.position;
        targetPosition = center;
        transform.Rotate(0,90,0);
        GameController.Instance.damageable = false;
        StartCoroutine(ArcMoveToCenter());
        Camera.main.GetComponent<CameraController>().alternateTarget = gameObject.transform;
        Camera.main.GetComponent<CameraController>().useAlternateTarget = true;
    }

    IEnumerator GremlinText() {
        gremlinText.enabled = true;
        gremlinText.gameObject.SetActive(true);
        yield return new WaitForSeconds (20f);
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
        StartCoroutine(OnReachedCenter());
    }

    IEnumerator OnReachedCenter()
    {
        healthBar.SetHealth(health, startHealth);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("reached center");
        flyingVFX.SetActive(false);
        Camera.main.GetComponent<CameraController>().useAlternateTarget = false;
        yield return new WaitForSeconds(2f);
        GameController.Instance.damageable = true;
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

        if(health <= 0 && !dead) {
                dead = true;
            Debug.Log("killed");
            StartCoroutine(killGoblin());
        }
    }
    }
    IEnumerator killGoblin() {
        boomSFX.Play();
        boomVFX.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        goblinGFX.SetActive(false);
        yield return new WaitForSeconds(boomSFX.clip.length - 0.5f);
        SceneManager.LoadScene("WinScene");
    }
}
