using UnityEngine;

public class BallistaTower : Tower
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowHead;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (arrowPrefab != null)
        {
            GameObject projectileInstace = Instantiate(arrowPrefab, arrowHead.position, Quaternion.identity);
            projectileInstace.GetComponent<ArrowProjectile>().SetTarget(target.transform);
        }
    }

    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();

        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
