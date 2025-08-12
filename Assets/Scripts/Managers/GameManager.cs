using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Health PlayerHealth;
    public CurrencyManager PlayerCurrency;
    public WaveManager WaveTracker;
    public ResultsManager ResultsTracker;
    public LevelManager LevelTracker;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        PlayerHealth = GetComponent<Health>();
        PlayerCurrency = GetComponent<CurrencyManager>();
        WaveTracker = GetComponent<WaveManager>();
        ResultsTracker = GetComponent<ResultsManager>();
        LevelTracker = GetComponent<LevelManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClearLevels();
        BuildLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.IsDead())
        {
            EndCurrentLevel(false);
        }
        else if(WaveTracker.GetIsLevelFinished())
        {
            EndCurrentLevel(true);
        }
    }

    private void BuildLevel(int index)
    {
        int startingPlayerCurrency = LevelTracker.GetLevelDataListItem(index).StartingPlayerCurrency;
        List<WaveData> levelWaveData = LevelTracker.GetLevelDataListItem(index).LevelWaveData;

        LevelTracker.BuildLevelGrid(index);
        PlayerCurrency.SetCurrencyHeld(startingPlayerCurrency);
        WaveTracker.StartWaves(levelWaveData);
    }

    private void ClearLevels()
    {
        LevelTracker.TurnOffAllLevelGrids();
    }

    private void EndCurrentLevel(bool isPlayerWinner)
    {
        ResultsTracker.SetIsPlayerWinner(isPlayerWinner);
        ResultsTracker.ShowResults();
    }
}
