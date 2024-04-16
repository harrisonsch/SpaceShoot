using UnityEngine;
using TMPro;
public class EnemySpawner : MonoBehaviour
{
         public TextMeshProUGUI shipsText;
        public int shipsKilled = 0;
    public GameObject enemyPrefab;
    public Transform playerCamera;  
    public float minSpawnDistance = 50f;  
    public float maxSpawnDistance = 100f;
    public float spawnRate = 5f;
    private float spawnTimer;
    public float spawnAngle = 45f;  
    public float maxVerticalAngle = 20f;  

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnRate;
        }
    }

    void SpawnEnemy()
    {
        float horizontalAngle = Random.Range(-spawnAngle, spawnAngle);
        float verticalAngle = Random.Range(-maxVerticalAngle / 2, maxVerticalAngle / 2);
        Quaternion spawnRotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * Quaternion.LookRotation(playerCamera.forward);
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = playerCamera.position + spawnRotation * Vector3.forward * distance;

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    public void shipDied() 
    {
        shipsKilled++;
        shipsText.text = "Ships Killed: " + shipsKilled.ToString();
    }
}