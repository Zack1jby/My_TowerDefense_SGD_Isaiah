using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endpoint;
    [SerializeField] private string animatorParam_IsWalking;
    [SerializeField] private int damage;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.SetDestination(endpoint.position);
        animator.SetBool(animatorParam_IsWalking, true);
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
    }

    private void ReachedEnd()
    {
        animator.SetBool(animatorParam_IsWalking, false);
        GameManager.Instance.playerHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
