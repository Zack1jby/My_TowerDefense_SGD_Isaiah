using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    private int currencyHeld;

    private void Awake()
    {
        UpdateCurrency(0);
    }

    public void UpdateCurrency(int currencyAdded)
    {
        currencyHeld += currencyAdded;
        currencyText.text = $"${currencyHeld}";
    }

    public int GetCurrencyHeld()
    {
        return currencyHeld;
    }
}
