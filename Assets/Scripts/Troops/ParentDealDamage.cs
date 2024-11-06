using UnityEngine;

public class ParentDealDamage : MonoBehaviour
{
    public Troop troop;

    public void DoDamage()
    {
        troop.PerformDamage();
    }
}
