using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int LootChances;

    private void Awake()
    {
      gameManager = this;
    }
}
