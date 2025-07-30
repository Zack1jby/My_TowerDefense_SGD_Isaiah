using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;

    private GameObject currentTowerPrefabToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPosition;
    [SerializeField] private float towerPlacementHeightOffset = 0.2f;

    [SerializeField] private bool isPlacingTower;
    private bool isTileSelected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacingTower)
        {
            Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, TileLayer))
            {
                towerPlacementPosition = hitInfo.transform.position + Vector3.up * towerPlacementHeightOffset;
                towerPreview.transform.position = towerPlacementPosition;

                isTileSelected = true;
                towerPreview.SetActive(true);
            }
            else
            {
                isTileSelected = false;
                towerPreview.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        PlaceTowerAction.Enable();
        PlaceTowerAction.performed += OnPlaceTower;
    }

    private void OnDisable()
    {
        PlaceTowerAction.performed -= OnPlaceTower;
        PlaceTowerAction.Disable();
    }

    public void StartPlacing(GameObject towerPrefab)
    {
        if (currentTowerPrefabToSpawn != towerPrefab)
        {
            isPlacingTower = true;
            currentTowerPrefabToSpawn = towerPrefab;
            if (towerPreview != null)
            {
                Destroy(towerPreview);
            }
            towerPreview = Instantiate(currentTowerPrefabToSpawn);
        }
    }

    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if (!isPlacingTower && isTileSelected)
        {
            return;
        }
        Instantiate(currentTowerPrefabToSpawn, towerPlacementPosition, Quaternion.identity);
        Destroy(towerPreview);
        currentTowerPrefabToSpawn = null;
        isPlacingTower = false;
    }
}
