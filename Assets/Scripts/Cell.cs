using UnityEngine;

public class Cell : MonoBehaviour
{
    public Troop troop;
    public bool occupied = false;
    public bool ghostOccupied = false;

    public GameObject ghostObj;

    private void Update()
    {
        if (ghostOccupied)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedCell = hit.collider.gameObject;

                // Check if the hit object is indeed a cell
                if (clickedCell == null || clickedCell.GetComponent<Cell>() != this)
                {
                    Destroy(ghostObj);
                    ghostOccupied = false;
                }
            }
        }
    }
}
