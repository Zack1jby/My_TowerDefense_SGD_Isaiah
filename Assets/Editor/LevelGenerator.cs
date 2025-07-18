using UnityEngine;
using UnityEditor;

public class LevelGenerator : EditorWindow
{
    private int gridSizeX = 10;
    private int gridSizeZ = 10;
    private GameObject tilePrefab;
    private Transform gridParent;
    private int sectionSpacing = 10;
    private GameObject[,] gridTiles;

    [MenuItem("Tools/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        gridSizeX = EditorGUILayout.IntField("Grid Size X", gridSizeX);
        gridSizeZ = EditorGUILayout.IntField("Grid Size Z", gridSizeZ);
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Tile Prefab", EditorStyles.boldLabel);
        tilePrefab = (GameObject) EditorGUILayout.ObjectField("Tile Prefab", tilePrefab, typeof(GameObject), false);
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Grid Parent", EditorStyles.boldLabel);
        gridParent = (Transform) EditorGUILayout.ObjectField("Grid Parent", gridParent, typeof(Transform), true);
        GUILayout.Space(sectionSpacing);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
        GUILayout.Space(sectionSpacing);

        if (GUILayout.Button("Clear Grid"))
        {
            ClearGrid();
        }
    }

    private void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile Prefab not assigned!");
            return;
        }
        gridTiles = new GameObject[gridSizeX, gridSizeZ];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 postion = new Vector3(x, 0, z);
                gridTiles[x, z] = (GameObject) PrefabUtility.InstantiatePrefab(tilePrefab, gridParent);
                gridTiles[x, z].transform.position = postion;
            }
        }
    }

    private void ClearGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                DestroyImmediate(gridTiles[x, z]);
            }
        }
    }
}
