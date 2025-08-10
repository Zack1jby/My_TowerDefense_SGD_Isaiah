using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public bool CanHoldEnemies;
    public List<GameObject> HeldEnemies;
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
                if (currentEnemyToSpawn.CanHoldEnemies)
                {
                    SpawnCarrierEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint, currentEnemyToSpawn.HeldEnemies);
                }
                else 
                {
                    SpawnEnemy(currentEnemyToSpawn.EnemyToSpawn, currentEnemyToSpawn.SpawnPoint, currentEnemyToSpawn.EndPoint);
                }
            }
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(endPoint);
    }

    public void SpawnCarrierEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint, List<GameObject> heldEnemies)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        CarrierEnemy enemy = enemyInstance.GetComponent<CarrierEnemy>();
        enemy.Initialize(endPoint, heldEnemies);
    }

    public int GetWaveCount()
    {
        return levelWaveData.Count;
    }
}
