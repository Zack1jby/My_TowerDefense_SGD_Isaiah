using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Health PlayerHealth;
    public CurrencyManager PlayerCurrency;
    public WaveManager WaveTracker;
    public ResultsManager ResultsTracker;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
