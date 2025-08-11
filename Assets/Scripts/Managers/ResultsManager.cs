using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOverDisplay;
    [SerializeField] private GameObject VictoryDisplay;
    [SerializeField] private GameObject ResultsDisplay;
    [SerializeField] private TextMeshProUGUI tallyHealthRecordText;
    [SerializeField] private TextMeshProUGUI tallyMoneyRecordText;
    [SerializeField] private TextMeshProUGUI tallyWavesRecordText;
    [SerializeField] private TextMeshProUGUI resultsRankText;
    private int rankEarnA = 1750;
    private int rankEarnB = 1400;
    private int rankEarnC = 1200;
    private int rankEarnD = 1000;
    private int tallyHealthScore;
    private int tallyMoneyScore;
    private int tallyWaveScore;
    private bool isPlayerWinner;

    private void Awake()
    {
        GameOverDisplay.SetActive(false);
        VictoryDisplay.SetActive(false);
        ResultsDisplay.SetActive(false);
    }

    private void Update()
    {
        
    }

    public bool SetIsPlayerWinner(bool isPlayerWinner)
    {
        return this.isPlayerWinner = isPlayerWinner;
    }

    public void ShowResults()
    {
        if (isPlayerWinner)
        {
            VictoryDisplay.SetActive(true);
        }
        else
        {
            GameOverDisplay.SetActive(true);
        }

        TallyHealthResults();
        TallyMoneyResults();
        TallyWaveResults();
        TallyRankResults();

        ResultsDisplay.SetActive(true);
    }

    private void TallyHealthResults()
    {
        int playerMaxHP = GameManager.Instance.PlayerHealth.GetMaxHealth();
        int playerLeftoverHP = GameManager.Instance.PlayerHealth.GetCurrentHealth();
        UpdateHealthResultsText(playerLeftoverHP, playerMaxHP);

        tallyHealthScore = playerLeftoverHP * 10;
    }

    private void TallyMoneyResults()
    {
        int currencyLeftover = GameManager.Instance.PlayerCurrency.GetCurrencyHeld();
        UpdateMoneyResultsText(currencyLeftover);

        tallyMoneyScore = currencyLeftover * 10;
    }

    private void TallyWaveResults()
    {
        int currentWave = GameManager.Instance.WaveTracker.GetCurrentWaveCount();
        int levelWaveCount = GameManager.Instance.WaveTracker.GetLevelWaveCount();
        UpdateWaveResultsText(currentWave, levelWaveCount);

        tallyWaveScore = (currentWave / levelWaveCount) * 1000;
    }

    private void TallyRankResults()
    {
        int tallyTotalScore = tallyHealthScore + tallyMoneyScore + tallyWaveScore;
        string rankEarned = "X";

        if (tallyTotalScore >= rankEarnA)
        {
            rankEarned = "A";
        }
        else if (tallyTotalScore >= rankEarnB)
        {
            rankEarned = "B";
        }
        else if (tallyTotalScore >= rankEarnC)
        {
            rankEarned = "C";
        }
        else if (tallyTotalScore >= rankEarnD)
        {
            rankEarned = "D";
        }
        else
        {
            rankEarned = "F";
        }

            UpdateRankResultsText(rankEarned);
    }

    private void UpdateHealthResultsText(int playerLeftoverHP, int playerMaxHP)
    {
        tallyHealthRecordText.text = $"{playerLeftoverHP} / {playerMaxHP}";
    }

    private void UpdateMoneyResultsText(int currencyLeftover)
    {
        tallyMoneyRecordText.text = $"{currencyLeftover}";
    }

    private void UpdateWaveResultsText(int currentWave, int levelWaveCount)
    {
        tallyWavesRecordText.text = $"{currentWave} / {levelWaveCount}";
    }

    private void UpdateRankResultsText(string rankEarned)
    {
        resultsRankText.text = $"RANK: {rankEarned}";
    }
}
