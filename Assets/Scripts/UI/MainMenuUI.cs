using CrazyGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            CrazySDK.Game.GameplayStop();
            SceneManager.LoadScene(1);
        });

    }

    private void Start()
    {
        CrazySDK.Game.GameplayStart();
    }

}
