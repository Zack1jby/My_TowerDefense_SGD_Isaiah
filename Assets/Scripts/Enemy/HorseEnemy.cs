using UnityEngine;
using UnityEngine.AI;

public class HorseEnemy : Enemy
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
