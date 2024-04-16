using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;    
    public Transform playerTransform; 
    public float spawnRadius = 30f;   
    public float spawnRate = 5f;      
    private float spawnTimer;

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
    // Calculate a random angle in radians
    float angle = Random.Range(0, 360) * Mathf.Deg2Rad;

    // Choose a random distance within a range
    float distance = Random.Range(spawnRadius * 0.8f, spawnRadius); // 80% to 100% of spawnRadius

    // Calculate the spawn position
    Vector3 spawnPosition = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * distance;
    spawnPosition += playerTransform.position;

    // Instantiate the enemy at the spawn position
    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
}
}