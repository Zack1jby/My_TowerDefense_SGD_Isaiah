using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 3f;
    protected Transform target;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target != null)
        {
            ReachTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        Destroy(gameObject);
    }

    protected abstract void ReachTarget();
}
