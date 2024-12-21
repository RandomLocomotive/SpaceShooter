using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRate = 2.5f;

    public float minXAxispawnValue = -8f;
    public float maxXAxisSpawnValue = 8f;

    public float yAxisSpawnValue = 4.5f;

    private float timeSinceLastAction = 0f;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
    }

    void Update()
    {
        timeSinceLastAction += Time.deltaTime;

        if (timeSinceLastAction >= spawnRate)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float xPosition = Random.Range(minXAxispawnValue, maxXAxisSpawnValue);
        Vector2 spawnPosition = new Vector2(xPosition, yAxisSpawnValue);

        // ROTACJA Z-90
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, -90f);

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation, this.transform);

        timeSinceLastAction = 0f;

        spawnedEnemies.Add(spawnedEnemy);
    }
}