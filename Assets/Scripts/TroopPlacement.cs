using UnityEngine;
using static GeneralResourceController;

public class TroopPlacement : MonoBehaviour
{
    public GridManager gridManager; // Reference to the GridManager
    public GameObject[] troopPrefabs; // Prefab of the troop to be placed

    private int currentTroopIndex = 0; // Index of the current troop prefab
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click to place troop
        {
            if (troopPrefabs[currentTroopIndex].GetComponent<Troop>().troopCost > GeneralResourceController.Instance.GetResourceAmount(ResourceType.Gold))
            {
                Debug.Log("Not enough gold to place this troop!");
                return;
            }
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the cell that was clicked
            GameObject clickedCell = GetCellAtPosition(mousePosition);
            if (clickedCell != null)
            {
                Cell cell = clickedCell.GetComponent<Cell>();
                if (cell.occupied)
                {
                    Debug.Log(clickedCell.name + " is occupied!");
                    return;
                }
                // Instantiate the troop at the snapped position and make it a child of the clicked cell
                GameObject troop = Instantiate(troopPrefabs[currentTroopIndex], mousePosition, Quaternion.identity, clickedCell.transform);
                cell.troop = troop.GetComponent<Troop>(); // Assign the troop to the cell
                cell.occupied = true; // Mark the cell as occupied
                troop.transform.localPosition = Vector3.zero; // Center the troop within the cell
                GeneralResourceController.Instance.DecreaseResource(GeneralResourceController.ResourceType.Gold, troop.GetComponent<Troop>().troopCost);
            }
        }
        else if (Input.GetMouseButtonDown(2)) // Middle click to remove troop
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Get the cell that was clicked
            GameObject clickedCell = GetCellAtPosition(mousePosition);
            if (clickedCell != null)
            {
                Cell cell = clickedCell.GetComponent<Cell>();
                if (!cell.occupied)
                {
                    Debug.Log("No troop to remove!");
                    return;
                }
                Destroy(cell.troop.gameObject); // Destroy the troop
                cell.troop = null; // Remove the troop reference from the cell
                cell.occupied = false; // Mark the cell as unoccupied
            }
        }
    }

    // Method to get the cell at the snapped position
    private GameObject GetCellAtPosition(Vector2 position)
    {
        // Iterate through the grid array in GridManager to find the corresponding cell
        for (int row = 0; row < gridManager.rows; row++)
        {
            for (int col = 0; col < gridManager.cols; col++)
            {
                GameObject cell = gridManager.gridArray[row, col];
                // Check if the position is within the bounds of the cell
                if (cell != null && cell.GetComponent<Collider2D>().bounds.Contains(position))
                {
                    return cell; // Return the clicked cell
                }
            }
        }
        return null; // No cell found
    }
}
