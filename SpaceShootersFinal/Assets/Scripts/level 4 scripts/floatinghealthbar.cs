using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatinghealthbar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] public new Camera camera;
    [SerializeField] public Transform target;
    [SerializeField] public Vector3 offset;

    // Start is called before the first frame update
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }
}
