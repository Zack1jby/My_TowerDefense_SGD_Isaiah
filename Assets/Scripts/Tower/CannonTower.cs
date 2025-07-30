using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject cannonBallPrefab;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (cannonBallPrefab != null)
        {
            GameObject projectileInstace = Instantiate(cannonBallPrefab, transform.position, Quaternion.identity);
            projectileInstace.GetComponent<CannonBallProjectile>().SetTarget(target.transform);
        }
    }

    protected override Enemy GetTargetEnemy()
    {
        return null;
    }
}
