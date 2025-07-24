using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        health.OnHealthStart += UpdateHealthBar;
    }

    void Start()
    {
        if (health != null)
        {
            health.OnHealthChanged += UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        text.text = $"{currentHealth} / {maxHealth}";
    }
}
