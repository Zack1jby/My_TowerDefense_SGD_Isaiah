using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Health PlayerHealth;
    public CurrencyManager PlayerCurrency;
    public WaveManager WaveTracker;
    public TowerPlaceManager TowerPlaceTracker;
    public InterfaceManager PlayerInterface;
    public ResultsManager ResultsTracker;
    public LevelManager LevelTracker;
    public MenuManager MenuTracker;
    private int currentLevelIndex;

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
        TowerPlaceTracker = GetComponent<TowerPlaceManager>();
        PlayerInterface = GetComponent<InterfaceManager>();
        ResultsTracker = GetComponent<ResultsManager>();
        LevelTracker = GetComponent<LevelManager>();
        MenuTracker = GetComponent<MenuManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameCleanUp();
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
        MenuTracker.DeactiveAllMenus();
        ResultsTracker.HideResults();
        GameCleanUp();

        int startingPlayerCurrency = LevelTracker.GetLevelDataListItem(index).StartingPlayerCurrency;
        List<WaveData> levelWaveData = LevelTracker.GetLevelDataListItem(index).LevelWaveData;

        LevelTracker.BuildLevelGrid(index);
        PlayerCurrency.SetCurrencyHeld(startingPlayerCurrency);
        PlayerInterface.SetUserInterfaceActive(true);
        WaveTracker.StartWaves(levelWaveData);
        Time.timeScale = 1;
    }

    private void GameCleanUp()
    {
        TowerPlaceTracker.CleanUpTowers();
        WaveTracker.CleanUpEnemies();
        LevelTracker.TurnOffAllLevelGrids();
    }

    private void EndCurrentLevel(bool isPlayerWinner)
    {
        PlayerInterface.SetUserInterfaceActive(false);
        ResultsTracker.SetIsPlayerWinner(isPlayerWinner);
        ResultsTracker.ShowResults();
        Time.timeScale = 0;
    }

    public void EnterLevel(int newLevelIndex)
    {
        if (currentLevelIndex < LevelTracker.GetLevelDataList().Count)
        {
            BuildLevel(newLevelIndex);
            currentLevelIndex = newLevelIndex;
        }
    }

    public void RestartCurrentLevel()
    {
        BuildLevel(currentLevelIndex);
    }

    public void EnterNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < LevelTracker.GetLevelDataList().Count)
        {
            BuildLevel(currentLevelIndex);
        }
        else
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        GameCleanUp();
        MenuTracker.ActivateMainMenu();
    }

    public void QuitGame()
    {
        ReturnToMainMenu();
    }
}
