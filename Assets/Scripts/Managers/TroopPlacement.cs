using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GeneralResourceController;

public class TroopPlacement : MonoBehaviour
{
    public GridManager gridManager; // Reference to the GridManager
    public Toggle[] TroopToggles;
    public GameObject[] troopPrefabs; // Prefab of the troop to be placed
    public GameObject[] ghostTroopPrefabs; // Prefab of the troop to be placed
    public float troopHealthPercentage = 0.5f; // Percentage of health to restore when refreshing troops

    //<List> of placed troops
    private List<Troop> placedTroops = new List<Troop>();
    [HideInInspector] public int currentTroopIndex = -1; // Index of the current troop prefab
    void Update()
    {
        if (currentTroopIndex != -1 && GeneralResourceController.Instance.runeIndex == -1)
        {
            for (int i = 0; i < gridManager.gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridManager.gridArray.GetLength(1); j++)
                {
                    Cell cell = gridManager.gridArray[i, j].GetComponent<Cell>();
                    if (!cell.occupied && troopPrefabs[currentTroopIndex].GetComponent<Troop>().troopCost <= GeneralResourceController.Instance.GetResourceAmount(ResourceType.Gold))
                    {
                        cell.GetComponent<SpriteRenderer>().color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.1f);
                    }
                    else
                    {
                        cell.GetComponent<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.1f);
                    }
                }
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedCell = hit.collider.gameObject;

                // Check if the hit object is indeed a cell
                if (clickedCell != null && clickedCell.GetComponent<Cell>() != null)
                {
                    Cell cell = clickedCell.GetComponent<Cell>();
                    if (!cell.ghostOccupied && !cell.occupied && troopPrefabs[currentTroopIndex].GetComponent<Troop>().troopCost <= GeneralResourceController.Instance.GetResourceAmount(ResourceType.Gold))
                    {
                        cell.ghostObj = Instantiate(ghostTroopPrefabs[currentTroopIndex], clickedCell.transform.position, Quaternion.identity, clickedCell.transform);
                        cell.ghostOccupied = true;
                    }
                }
            }
        }
        else if (Instance.runeIndex != -1 && currentTroopIndex == -1)
        {
            for (int i = 0; i < gridManager.gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridManager.gridArray.GetLength(1); j++)
                {
                    Cell cell = gridManager.gridArray[i, j].GetComponent<Cell>();
                    if (cell.occupied && Instance.Runes[Instance.runeIndex] >= 1 && (cell.troop.GetComponent<Archer>() || cell.troop.GetComponent<Knight>()) && cell.troop.variant != cell.troop.GetVariant(Instance.runeIndex))
                    {
                        cell.GetComponent<SpriteRenderer>().color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.1f);
                    }
                    else
                    {
                        cell.GetComponent<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.1f);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < gridManager.gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridManager.gridArray.GetLength(1); j++)
                {
                    Cell cell = gridManager.gridArray[i, j].GetComponent<Cell>();
                    if (cell.GetComponent<SpriteRenderer>().color != Color.clear)
                    {
                        cell.GetComponent<SpriteRenderer>().color = Color.clear;
                    }
                }
            }
        }
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
                    TroopToggles[currentTroopIndex].isOn = false; // Deselect the troop toggle
                    currentTroopIndex = -1; // Reset the current troop index
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && Instance.runeIndex != -1)
        {

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

                    if (cell.occupied && Instance.Runes[Instance.runeIndex] >= 1 && (cell.troop.GetComponent<Archer>() || cell.troop.GetComponent<Knight>()) && cell.troop.variant != cell.troop.GetVariant(Instance.runeIndex))
                    {
                        cell.troop.SetVariant(Instance.runeIndex);
                        Instance.Runes[Instance.runeIndex]--;
                    }
                    else
                    {
                        Instance.runeIndex = -1;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(2)) // Middle click to remove troop
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
        GeneralResourceController.Instance.runeIndex = -1;
    }

    public void TroopHealthRefresh()
    {
        foreach (Troop troop in placedTroops)
        {
            troop.Heal(troopHealthPercentage);
        }
    }
}
