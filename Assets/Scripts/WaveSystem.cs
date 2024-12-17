using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int count;
    }

    [System.Serializable]
    public class WaveData
    {
        public List<EnemySpawnInfo> enemySpawnInfos;
        public float timeBetweenSpawns = 2f;
    }

    [Header("Wave Settings")]
    public List<WaveData> waves;
    public float timeBetweenWaves = 5f;

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public bool autoStart = true; 

    private int currentWave = 0;
    private bool isSpawning = false;

    void Start()
    {
        if (autoStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnWaves());
        }
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        isSpawning = false;
    }

    private IEnumerator SpawnWaves()
    {
        isSpawning = true;

        for (currentWave = 0; currentWave < waves.Count; currentWave++)
        {
            Debug.Log($"Starting Wave {currentWave + 1}");
            yield return StartCoroutine(SpawnWave(waves[currentWave]));
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves completed!");
        isSpawning = false;
    }

    private IEnumerator SpawnWave(WaveData wave)
    {
        foreach (var spawnInfo in wave.enemySpawnInfos)
        {
            for (int i = 0; i < spawnInfo.count; i++)
            {
                SpawnEnemy(spawnInfo.enemyPrefab);
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        }
    }
}
