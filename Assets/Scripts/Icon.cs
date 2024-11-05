using UnityEngine;

public class Icon : MonoBehaviour
{
    public int troopIndex;

    public void SetTroopIndex(bool on)
    {
        if (on)
            GeneralResourceController.Instance.troopPlacement.UpdateTroopIndex(troopIndex);
        else
            GeneralResourceController.Instance.troopPlacement.UpdateTroopIndex(-1);
    }
}
