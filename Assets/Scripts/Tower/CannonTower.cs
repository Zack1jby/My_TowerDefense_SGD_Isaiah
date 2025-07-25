using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject cannonBallPrefab;
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        
    }

    protected override Enemy GetTargetEnemy()
    {
        return null;
    }
}
