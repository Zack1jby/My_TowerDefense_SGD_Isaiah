using UnityEngine;

public class TowerPrefabHolder : MonoBehaviour
{
    [SerializeField] private GameObject heldTowerPrefab;

    public GameObject GetHeldTowerPrefab()
    {
        return heldTowerPrefab;
    }
}
