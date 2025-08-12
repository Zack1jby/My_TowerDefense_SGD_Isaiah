using UnityEngine;
using System.Collections.Generic;
using TMPro;

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
            PrepareAllBuildButtonTexts(buildTowerButtons);
        }
    }

    private void PrepareBuildButtonText(BuildTowerButton buildTowerButton)
    {
        TextMeshProUGUI buildButtonText = buildTowerButton.GetBuildButtonText();
        GameObject towerPrefab = buildTowerButton.GetHeldTowerPrefab();
        int towerPrefabCurrencyCost = towerPrefab.GetComponent<Tower>().GetTowerCost();

        buildButtonText.text = $"${towerPrefabCurrencyCost}";
    }

    private void PrepareAllBuildButtonTexts(List<BuildTowerButton> allBuildTowerButtons)
    {
        foreach (BuildTowerButton buildTowerButton in allBuildTowerButtons)
        {
            PrepareBuildButtonText(buildTowerButton);
        }
    }
}
