using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform barrellHead;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (cannonBallPrefab != null)
        {
            GameObject projectileInstace = Instantiate(cannonBallPrefab, barrellHead.position, Quaternion.identity);
            projectileInstace.GetComponent<CannonBallProjectile>().SetTarget(target.transform);
        }
    }

    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();

        Enemy healthiestEnemy = null;
        int highestCurrentHealth = int.MinValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            int enemyCurrentHealth = enemy.GetCurrentHealth();
            if (enemyCurrentHealth > highestCurrentHealth)
            {
                highestCurrentHealth = enemyCurrentHealth;
                healthiestEnemy = enemy;
            }
        }
        return healthiestEnemy;
    }
}
