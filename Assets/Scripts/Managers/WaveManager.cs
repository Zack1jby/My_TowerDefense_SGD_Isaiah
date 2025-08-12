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
    [SerializeField] private GameObject enemyContainer;
    private List<WaveData> currentLevelWaveData;
    private int currentWaveCount;
    private bool isNextWaveReady = true;
    private bool isLevelFinished;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void StartWaves(List<WaveData> levelWaveData)
    {
        isLevelFinished = false;
        currentWaveCount = 0;
        currentLevelWaveData = levelWaveData;
        StartCoroutine(StartLevelWaves());
    }

    private IEnumerator StartLevelWaves()
    {
        foreach (WaveData currentWave in currentLevelWaveData)
        {
            yield return new WaitUntil(() => isNextWaveReady);
            StartCoroutine(StartWave(currentWave.EnemyData));
            currentWaveCount++;
        }
        yield return new WaitUntil(() => isNextWaveReady);
        isLevelFinished = true;
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, enemyContainer.transform);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(endPoint);
    }

    public void SpawnCarrierEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint, List<GameObject> heldEnemies)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, enemyContainer.transform);
        CarrierEnemy enemy = enemyInstance.GetComponent<CarrierEnemy>();
        enemy.Initialize(endPoint, heldEnemies);
    }

    public int GetLevelWaveCount()
    {
        return currentLevelWaveData.Count;
    }

    private IEnumerator StartWave(List<SpawnData> currentWaveEnemyData)
    {
        isNextWaveReady = false;
        foreach (SpawnData currentEnemyToSpawn in currentWaveEnemyData)
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
        isNextWaveReady = true;
    }

    public int GetCurrentWaveCount()
    {
        return currentWaveCount;
    }

    public bool GetIsLevelFinished()
    {
        return isLevelFinished;
    }

    public void CleanUpEnemies()
    {
        Enemy[] enemiesToClean = enemyContainer.GetComponentsInChildren<Enemy>();
        foreach (Enemy e in enemiesToClean)
        {
            Destroy(e.gameObject);
        }
    }
}
