using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject userInterface;
    [SerializeField] private List<BuildTowerButton> buildTowerButtons;

    private void Awake()
    {
        SetUserInterfaceActive(false);
    }

    public void SetUserInterfaceActive(bool active)
    {
        userInterface.SetActive(active);
        if (active)
        {
            PrepareAllBuildButtonsUnlock(buildTowerButtons);
            PrepareAllBuildButtonTexts(buildTowerButtons);
        }
    }

    private void PrepareBuildButtonText(BuildTowerButton buildTowerButton)
    {
        TextMeshProUGUI buildButtonText = buildTowerButton.GetBuildButtonText();
        GameObject towerPrefab = buildTowerButton.GetHeldTowerPrefab();

        if (CheckButtonUnlocked(buildTowerButton)) 
        {
            int towerPrefabCurrencyCost = towerPrefab.GetComponent<Tower>().GetTowerCost();

            buildButtonText.text = $"${towerPrefabCurrencyCost}";
        }
        else
        {
            buildButtonText.text = $"LOCKED";
        }
    }

    private void PrepareAllBuildButtonTexts(List<BuildTowerButton> allBuildTowerButtons)
    {
        foreach (BuildTowerButton buildTowerButton in allBuildTowerButtons)
        {
            PrepareBuildButtonText(buildTowerButton);
        }
    }

    private void PrepareAllBuildButtonsUnlock(List<BuildTowerButton> allBuildTowerButtons)
    {
        foreach (BuildTowerButton currentBuildTowerButton in allBuildTowerButtons)
        {
            Button buildTowerButton = currentBuildTowerButton.GetComponent<Button>();
            if (CheckButtonUnlocked(currentBuildTowerButton))
            {
                buildTowerButton.enabled = true;
            }
            else
            {
                buildTowerButton.enabled = false;
            }
        }
    }

    private bool CheckButtonUnlocked(BuildTowerButton buildTowerButton)
    {
        int towerPrefabRank = buildTowerButton.GetHeldTowerPrefab().GetComponent<Tower>().GetTowerRank();
        int currentLevelMaxRank = GameManager.Instance.GetCurrentLevelData().MaxTowerRank;
        return towerPrefabRank <= currentLevelMaxRank;
    }

    public List<BuildTowerButton> GetBuildTowerButtons()
    {
        return buildTowerButtons;
    }
}
