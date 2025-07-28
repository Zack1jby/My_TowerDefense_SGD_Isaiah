using UnityEngine;

public class ArrowProjectile : Projectile
{
    protected override void ReachTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }
}
