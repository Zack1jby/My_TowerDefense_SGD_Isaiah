using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnData
{
    public GameObject enemyToSpawn;
    public float TimeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

[System.Serializable]
public struct WaveData
{
    public float TimeBeforeWave;
    public List<SpawnData> EnemyData;
}

public class WaveManager : MonoBehaviour
{
    public List<WaveData> levelWaveData;

    void Start()
    {
        StartLevel();
    }

    void Update()
    {
        
    }

    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        foreach (WaveData currentWave in levelWaveData)
        {
            yield return new WaitForSeconds(currentWave.TimeBeforeWave);
            foreach (SpawnData currentEnemyToSpawn in currentWave.EnemyData)
            {
                yield return new WaitForSeconds(currentEnemyToSpawn.TimeBeforeSpawn);
                SpawnEnemy(currentEnemyToSpawn.enemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
            }
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(endPoint);
    }
}
