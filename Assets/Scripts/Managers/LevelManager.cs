using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;

[System.Serializable]
public struct LevelData
{
    public GameObject LevelGrid;
    public int StartingPlayerCurrency;
    public List<WaveData> LevelWaveData;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelData> LevelList;
    [SerializeField] private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        TurnOffAllLevelGrids();
    }

    public void BuildLevelGrid(int index)
    {
        LevelList[index].LevelGrid.SetActive(true);
        navMeshSurface.gameObject.SetActive(true);
        navMeshSurface.BuildNavMesh();
    }

    public void TurnOffAllLevelGrids()
    {
        navMeshSurface.gameObject.SetActive(false);
        foreach (LevelData currentLevel in LevelList)
        {
            if ((currentLevel.LevelGrid != null) && (currentLevel.LevelGrid.activeSelf))
            {
                currentLevel.LevelGrid.SetActive(false);
            }
        }
    }

    public List<LevelData> GetLevelDataList()
    {
        return LevelList;
    }

    public LevelData GetLevelDataListItem(int index)
    {
        return LevelList[index];
    }
}
