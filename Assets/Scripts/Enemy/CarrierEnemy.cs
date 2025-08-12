using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CarrierEnemy : Enemy
{
    protected List<GameObject> heldEnemies;
    protected Transform gameEnemyContainer;
    protected float spawnOffsetRangeX = .6f;
    protected float spawnOffsetRangeZ = .6f;

    public void Initialize(Transform inputEndPoint, List<GameObject> heldEnemies, Transform gameEnemyContainer)
    {
        base.Initialize(inputEndPoint);
        this.heldEnemies = heldEnemies;
        this.gameEnemyContainer = gameEnemyContainer;
    }

    protected override void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachedEnd();
            }
        }
        if (IsDead())
        {
            DropCurrency();
            ReleaseHeldEnemies();
            Destroy(gameObject);
        }
    }

    protected void ReleaseHeldEnemies()
    {
        foreach (GameObject currentEnemy in heldEnemies)
        {
            SpawnEnemy(currentEnemy);
        }
    }

    protected void SpawnEnemy(GameObject enemyPrefab)
    {
        float spawnOffsetX = Random.Range(-spawnOffsetRangeX, spawnOffsetRangeX);
        float spawnOffsetZ = Random.Range(-spawnOffsetRangeZ, spawnOffsetRangeZ);
        Vector3 spawnPos = new Vector3(transform.position.x + spawnOffsetX, transform.position.y, transform.position.z + spawnOffsetZ);
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPos, transform.rotation, gameEnemyContainer);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(endPoint);
    }
}
