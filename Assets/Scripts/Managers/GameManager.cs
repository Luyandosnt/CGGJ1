using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Chances;

    private void Awake()
    {
      instance = this;
    }
}
