using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;
    [SerializeField] protected Transform endPoint;
    [SerializeField] protected string animatorParam_IsWalking;
    [SerializeField] protected float movementSpeed = 3.5f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected int maxHealth = 10;
    protected int currentHealth;
    [SerializeField] protected int currencyDropAmount;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        agent.speed = movementSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        animator.SetBool(animatorParam_IsWalking, true);
    }

    public void Initialize(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachedEnd();
            }
        }
        if (IsDead())
        {
            DropCurrency();
            Destroy(gameObject);
        }
    }

    protected virtual void ReachedEnd()
    {
        animator.SetBool(animatorParam_IsWalking, false);
        DealDamageToPlayer();
        Destroy(gameObject);
    }

    protected void DealDamageToPlayer()
    {
        GameManager.Instance.PlayerHealth.TakeDamage(damage);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int receivedDamage)
    {
        currentHealth -= receivedDamage;
    }

    public void DropCurrency()
    {
        GameManager.Instance.PlayerCurrency.UpdateCurrency(currencyDropAmount);
    }
}
