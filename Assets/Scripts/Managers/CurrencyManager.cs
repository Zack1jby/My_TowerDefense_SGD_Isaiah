using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    private int currencyHeld;

    private void Awake()
    {
        UpdateCurrencyText(0);
    }

    public void UpdateCurrencyText(int currencyAdded)
    {
        currencyHeld += currencyAdded;
        currencyText.text = $"${currencyHeld}";
    }
}
