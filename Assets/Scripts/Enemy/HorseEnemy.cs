using UnityEngine;
using UnityEngine.AI;

public class HorseEnemy : Enemy
{
    protected override void Start()
    {
        return;
    }

    protected override void ReachedEnd()
    {
        DealDamageToPlayer();
        Destroy(gameObject);
    }
}
