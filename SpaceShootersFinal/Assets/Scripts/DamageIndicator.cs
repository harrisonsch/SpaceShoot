using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Text text;
    public float lifetime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;
    public float textX = 50f;
    public float textY = 50f;
    public float textZ = 30f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        float range = Random.Range(minDist, maxDist);
        float dist = Random.Range(minDist, maxDist);
        iniPos = transform.position + (new Vector3(range, range, 0f));
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        if (timer > lifetime) Destroy(gameObject);
        else if (timer > fraction) text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(textX,textY,textZ), Mathf.Sin(timer / lifetime));
    }

    public void SetDamageText(float damage)
    {
        text.text = damage.ToString();
    }
}