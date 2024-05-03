using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Image healthBarImage;
    public Transform camTransform;
    public bool looking = true;

   

    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

     void LateUpdate()
    {
        if(looking){

        transform.LookAt(transform.position + camTransform.forward);
        }
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        Debug.Log("filling to " + currentHealth + " out of " + maxHealth + " ratio is now " + currentHealth/maxHealth);
        healthBarImage.fillAmount = currentHealth / maxHealth;
        Debug.Log("filled to " + healthBarImage.fillAmount);
    }
}