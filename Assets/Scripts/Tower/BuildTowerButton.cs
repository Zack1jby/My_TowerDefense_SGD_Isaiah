using UnityEngine;
using TMPro;

public class BuildTowerButton : MonoBehaviour
{
    [SerializeField] private GameObject heldTowerPrefab;
    [SerializeField] private TextMeshProUGUI buildButtonText;

    public GameObject GetHeldTowerPrefab()
    {
        return heldTowerPrefab;
    }

    public TextMeshProUGUI GetBuildButtonText()
    {
        return buildButtonText;
    }
}
