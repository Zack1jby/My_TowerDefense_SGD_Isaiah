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
    public List<SpawnData> EnemyData;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject startWaveButton;
    private List<WaveData> currentLevelWaveData;
    private int currentWaveCount;
    private bool isCurrentWaveDoneSpawning;
    private bool isNextWaveReady;
    private bool isLevelFinished;

    public void StartWaves(List<WaveData> levelWaveData)
    {
        SetIsNextWaveReady(false);
        isLevelFinished = false;
        isCurrentWaveDoneSpawning = true;
        currentWaveCount = 0;
        currentLevelWaveData = levelWaveData;
        StopCoroutine(StartLevelWaves());
        StartCoroutine(StartLevelWaves());
    }

    private IEnumerator StartLevelWaves()
    {
        SetStartWaveButtonActive(true);
        foreach (WaveData currentWave in currentLevelWaveData)
        {
            yield return new WaitUntil(() => isNextWaveReady);
            StartCoroutine(StartWave(currentWave.EnemyData));
        }
        yield return new WaitUntil(() => GetIsWaveFinished());
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
        enemy.Initialize(endPoint, heldEnemies, enemyContainer.transform);
    }

    public int GetLevelWaveCount()
    {
        return currentLevelWaveData.Count;
    }

    private IEnumerator StartWave(List<SpawnData> currentWaveEnemyData)
    {
        SetStartWaveButtonActive(false);
        SetIsNextWaveReady(false);
        isCurrentWaveDoneSpawning = false;
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
        currentWaveCount++;
        isCurrentWaveDoneSpawning = true;
        yield return new WaitUntil(() => GetIsWaveFinished());
        if (!IsOnLastWave()) 
        {
            SetStartWaveButtonActive(true);
        }
    }

    private void SetStartWaveButtonActive(bool active)
    {
        startWaveButton.SetActive(active);
    }

    public void SetIsNextWaveReady(bool isNextWaveReady)
    {
        this.isNextWaveReady = isNextWaveReady;
    }

    public int GetCurrentWaveCount()
    {
        return currentWaveCount;
    }

    private bool IsOnLastWave()
    {
        return currentWaveCount == GetLevelWaveCount();
    }

    private bool GetIsWaveFinished()
    {
        return isCurrentWaveDoneSpawning && enemyContainer.GetComponentsInChildren<Enemy>().Length == 0;
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
