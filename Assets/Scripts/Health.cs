using UnityEngine;

public class Health : MonoBehaviour
{
    public event System.Action<int, int> OnHealthStart;
    public event System.Action<int, int> OnHealthChanged;

    [SerializeField] private int maxHealth = 20;
    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    private void Start()
    {
        OnHealthStart?.Invoke(currentHealth, maxHealth);
    }

    public bool IsDead()
    {
        return currentHealth > 0;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
        Debug.Log($"Current Health: {currentHealth}");
    }
}
