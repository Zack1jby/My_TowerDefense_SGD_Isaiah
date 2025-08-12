using UnityEngine;
using System.Collections;

public class WizardTower : Tower
{
    [SerializeField] private GameObject magicRadiusPrefab;
    [SerializeField] protected float slowedDebuffStrength = .7f;
    [SerializeField] protected float slowedDebuffCooldown = 1f;

    protected void Awake()
    {
        SetMagicRadiusActive(false);
    }

    protected override void Update()
    {
        ClearDestroyedEnemies();

        if (AreEnemiesInRange())
        {
            if (!IsMagicRadiusActive())
            {
                SetMagicRadiusActive(true);
            }
            SlowDownEnemies();
            currentFireCooldown = FireCooldown;
        }
        else if (IsMagicRadiusActive())
        {
            SetMagicRadiusActive(false);
        }
    }

    protected void SlowDownEnemies()
    {
        foreach (Enemy currentTargetEnemy in enemiesInRange)
        {
            FireAt(currentTargetEnemy);
        }
    }

    protected override void FireAt(Enemy target)
    {
        if (target != null && !target.GetIsSlowed())
        {
            StartCoroutine(ApplySlowedDebuff(target));
        }
    }

    protected IEnumerator ApplySlowedDebuff(Enemy target)
    {
        target.SetIsSlowed(true);
        target.SetCurrentMovementSpeed(slowedDebuffStrength * target.GetCurrentMovementSpeed());
        yield return new WaitForSeconds(slowedDebuffCooldown);
        if (target != null) 
        {
            target.ResetCurrentMovementSpeed();
            target.SetIsSlowed(false);
        }
    }

    protected bool IsMagicRadiusActive()
    {
        return magicRadiusPrefab.activeSelf;
    }

    protected void SetMagicRadiusActive(bool isActive)
    {
        magicRadiusPrefab.SetActive(isActive);
    }

    protected bool AreEnemiesInRange()
    {
        return enemiesInRange.Count > 0;
    }

    protected override Enemy GetTargetEnemy()
    {
        return null;
    }
}
