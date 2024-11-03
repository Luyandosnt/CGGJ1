using UnityEngine;
using VInspector;

public class GridManager : MonoBehaviour
{
    public int rows = 6;
    public int cols = 10;
    public float cellSize = 1f; // Adjust cell size as needed

    public GameObject cellPrefab; // Assign a cell prefab (e.g., an empty sprite or square image)

    public GameObject[,] gridArray; // Array to hold all cell objects

    void Start()
    {
        GenerateGrid();
    }

    [Button]
    void GenerateGrid()
    {
        ClearGrid();
        gridArray = new GameObject[rows, cols];

        // Calculate the grid's total width and height
        float gridWidth = cols * cellSize;
        float gridHeight = rows * cellSize;

        // Calculate the offset to center the grid
        Vector2 offset = new Vector2(-gridWidth / 2 + cellSize / 2, -gridHeight / 2 + cellSize / 2);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Calculate cell position and apply the offset to center the grid
                Vector2 cellPosition = new Vector2(col * cellSize, row * cellSize) + offset;

                // Instantiate the cellPrefab at the calculated position
                GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform);

                // Name each cell for easier debugging
                cell.name = "Cell_" + row + "_" + col;

                cell.AddComponent<Cell>(); // Add the Cell script to the cell object

                // Store reference in gridArray
                gridArray[row, col] = cell;
            }
        }
    }

    [Button]
    void ClearGrid()
    {
        if (gridArray == null)
            return;

        // Destroy all cell objects
        foreach (GameObject cell in gridArray)
        {
            if (!Application.isPlaying)
                DestroyImmediate(cell);
            else
                Destroy(cell);
        }
    }

}
