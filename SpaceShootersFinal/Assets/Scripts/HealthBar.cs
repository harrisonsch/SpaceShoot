using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    public Transform camTransform;

   

    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

     void LateUpdate()
    {
        transform.LookAt(transform.position + camTransform.forward);
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }
}