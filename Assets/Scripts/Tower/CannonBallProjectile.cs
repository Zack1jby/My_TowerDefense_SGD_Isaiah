using UnityEngine;

public class CannonBallProjectile : Projectile
{
    protected override void ReachTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }
}
