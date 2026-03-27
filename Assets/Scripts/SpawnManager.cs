using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject[] powerUpPrefabs;
    public Wave[] waves;

    private int currentWave = 0;
    private Transform[] waveSpawnPoints;

    private Coroutine spawnCoroutine;

    void Start()
    {
        StartCoroutine(WaveManager());
    }

    IEnumerator WaveManager()
    {
        while (currentWave < waves.Length)
        {
            Debug.Log("Starting Wave: " + currentWave);
            
            // Select random spawn points for this wave
            SelectWaveSpawnPoints(currentWave);
            
            // Spawn powerups at wave start
            for (int i = 0; i < waves[currentWave].numberOfPowerUp; i++)
            {
                RandomPowerUp();
            }

            spawnCoroutine = StartCoroutine(SpawnRoutine(currentWave));
            yield return spawnCoroutine;

            currentWave++;
        }
        Debug.Log("All waves complete");
    }

    void SelectWaveSpawnPoints(int waveIndex)
    {
        int count = waves[waveIndex].numberOfRandomSpawnPoint;
        waveSpawnPoints = new Transform[count];

        // Create list of all available spawn points
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        // Randomly select 'count' spawn points
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            // Debug.Log("Selected spawn point: " + availableSpawnPoints[randomIndex].name);
            waveSpawnPoints[i] = availableSpawnPoints[randomIndex];
            availableSpawnPoints.RemoveAt(randomIndex);
        }
    }

    void RandomSpawn(int waveIndex)
    {
        if (waveSpawnPoints == null || waveSpawnPoints.Length == 0) return;
        
        var spawnPoint = waveSpawnPoints[Random.Range(0, waveSpawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    void RandomPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

        Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine(int waveIndex)
    {
        yield return new WaitForSeconds(waves[waveIndex].delayStart);

        for (int i = 0; i < waves[waveIndex].totalSpawnEnemies; i++)
        {
            RandomSpawn(waveIndex);
            yield return new WaitForSeconds(waves[waveIndex].spawnInterval);
        }
    }
}
