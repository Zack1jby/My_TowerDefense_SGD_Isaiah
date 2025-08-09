using UnityEngine;

public class CarEnemy : CarrierEnemy
{
    protected override void Start()
    {
        agent.SetDestination(endPoint.position);
    }

    protected override void ReachedEnd()
    {
        GameManager.Instance.PlayerHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
