using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endpoint;
    [SerializeField] private string animatorParam_IsWalking;
    [SerializeField] private int damage;
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.SetDestination(endpoint.position);
        animator.SetBool(animatorParam_IsWalking, true);
    }

    public void Initialize(Transform inputEndPoint)
    {
        endpoint = inputEndPoint;
        agent.SetDestination(endpoint.position);
    }

    // Update is called once per frame
    void Update()
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
            Destroy(gameObject);
        }
    }

    private void ReachedEnd()
    {
        animator.SetBool(animatorParam_IsWalking, false);
        GameManager.Instance.playerHealth.TakeDamage(damage);
        Destroy(gameObject);
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
}
