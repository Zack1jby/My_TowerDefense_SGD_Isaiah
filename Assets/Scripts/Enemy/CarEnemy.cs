using UnityEngine;

public class CarEnemy : CarrierEnemy
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
