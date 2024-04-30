using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Lvl1Boss : MonoBehaviour
{
        public float health = 200f;
    public float startHealth;
    public GameObject damageText;
    public Transform spawnPos;
    public Vector3 indicatorSize = new Vector3(3,3,3);
    public TextMeshProUGUI healthText;
    public AudioSource audioSource;
    public int bounty = 15;
    public bool winning = false;
    public AudioSource hitSFX;
    public bool dmgSound = false;
    // Start is called before the first frame update

    void Start() {
        startHealth = health;
    }

    void Update () {
        if(healthText != null) {

                healthText.text = "Boss: " + health.ToString();
        }

    }
    public void Damage(float value)
    {
        
        health -= value;
        // if (healthBar != null)
        // {
        //         healthBar.SetHealth(health, startHealth); 
        // }
        Debug.Log("hit " + gameObject.name +  " for " + value + " health is now " + health);
        // Vector3 temp = new Vector3(60f, 0, 0);
        DamageIndicator indicator = Instantiate(damageText, spawnPos.position, Quaternion.identity).GetComponent<DamageIndicator>();
        if (!hitSFX.isPlaying && dmgSound) {

        hitSFX.Play();
        }
        indicator.SetDamageText(value);
        indicator.transform.localScale = indicatorSize;
        if(healthText != null) 
        {

        healthText.text = "Boss: " + health.ToString();
        }

        if(health <= 0) {
            Debug.Log("killed");
            if(audioSource != null) {
                audioSource.Play();
            }
            Destroy(gameObject);
            GameController.Instance.balance += bounty;
            if(winning) {
                Destroy(GameController.Instance.gameObject);
                SceneManager.LoadScene("WinScene");
            } else {

            SceneManager.LoadScene("ShopScene");
            }
        }
    }
}
