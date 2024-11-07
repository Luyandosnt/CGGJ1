using System.Collections.Generic;
using UnityEngine;
using static GeneralResourceController;

public class TroopPlacement : MonoBehaviour
{
    public GridManager gridManager; // Reference to the GridManager
    public GameObject[] troopPrefabs; // Prefab of the troop to be placed
    public float troopHealthPercentage = 0.5f; // Percentage of health to restore when refreshing troops

    //<List> of placed troops
    private List<Troop> placedTroops = new List<Troop>();
    private int currentTroopIndex = -1; // Index of the current troop prefab
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentTroopIndex != -1) // Left click to place troop
        {
            if (troopPrefabs[currentTroopIndex].GetComponent<Troop>().troopCost > GeneralResourceController.Instance.GetResourceAmount(ResourceType.Gold))
            {
                Debug.Log("Not enough gold to place this troop!");
                return;
            }

            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedCell = hit.collider.gameObject;

                // Check if the hit object is indeed a cell
                if (clickedCell != null && clickedCell.GetComponent<Cell>() != null)
                {
                    Cell cell = clickedCell.GetComponent<Cell>();
                    if (cell.occupied)
                    {
                        Debug.Log(clickedCell.name + " is occupied!");
                        return;
                    }

                    // Instantiate the troop at the cell’s position
                    GameObject troop = Instantiate(troopPrefabs[currentTroopIndex], clickedCell.transform.position, Quaternion.identity, clickedCell.transform);
                    cell.troop = troop.GetComponent<Troop>(); // Assign the troop to the cell
                    placedTroops.Add(troop.GetComponent<Troop>()); // Add the troop to the list of placed troops
                    cell.occupied = true; // Mark the cell as occupied
                    troop.transform.localPosition = Vector3.zero; // Center the troop within the cell
                    GeneralResourceController.Instance.DecreaseResource(GeneralResourceController.ResourceType.Gold, troop.GetComponent<Troop>().troopCost);
                }
            }
        }
        else if (Input.GetMouseButtonDown(2)) // Middle click to remove troop
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedCell = hit.collider.gameObject;

                if (clickedCell != null && clickedCell.GetComponent<Cell>() != null)
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
    }

    public void UpdateTroopIndex(int index)
    {
        currentTroopIndex = index;
    }

    public void TroopHealthRefresh()
    {
        foreach (Troop troop in placedTroops)
        {
            troop.Heal(troopHealthPercentage);
        }
    }
}
