using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Range(0, 100)] public float LootChances;

    private void Awake()
    {
      gameManager = this;
    }
}
