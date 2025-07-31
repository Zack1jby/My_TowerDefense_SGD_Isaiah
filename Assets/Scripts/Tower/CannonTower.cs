using UnityEngine;
using System.Collections;

public class CannonTower : Tower
{
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform barrelHead;
    [SerializeField] private float effectsTime = .05f;
    private Light barrelHeadLight;

    private void Awake()
    {
        barrelHeadLight = barrelHead.GetComponent<Light>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (cannonBallPrefab != null)
        {
            StartCoroutine(FireAtEffects());
            GameObject projectileInstace = Instantiate(cannonBallPrefab, barrelHead.position, Quaternion.identity);
            projectileInstace.GetComponent<CannonBallProjectile>().SetTarget(target.transform);
        }
    }

    private IEnumerator FireAtEffects()
    {
        barrelHeadLight.enabled = true;
        yield return new WaitForSeconds(effectsTime);
        barrelHeadLight.enabled = false;
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
