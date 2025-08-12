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
        UpdateCurrencyText();
    }

    public void SetCurrencyHeld(int newHeldCurrencyAmount)
    {
        currencyHeld = newHeldCurrencyAmount;
        UpdateCurrencyText();
    }

    public int GetCurrencyHeld()
    {
        return currencyHeld;
    }

    private void UpdateCurrencyText()
    {
        currencyText.text = $"${currencyHeld}";
    }
}
