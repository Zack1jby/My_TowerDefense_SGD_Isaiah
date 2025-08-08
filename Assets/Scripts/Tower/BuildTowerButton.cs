using UnityEngine;

public class BuildTowerButton : MonoBehaviour
{
    [SerializeField] private GameObject heldTowerPrefab;

    public GameObject GetHeldTowerPrefab()
    {
        return heldTowerPrefab;
    }
}
