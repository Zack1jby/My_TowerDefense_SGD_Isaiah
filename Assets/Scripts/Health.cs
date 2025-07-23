using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
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
        }
        Debug.Log($"Current Health: {currentHealth}");
    }
}
