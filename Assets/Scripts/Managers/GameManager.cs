using UnityEngine.UI;
using UnityEngine;
using TMPro;
using CrazyGames;

public class GameManager : MonoBehaviour
{
    public AudioManager audioManager;
    public UpgradeManager upgradeManager;
    public GameObject nextWaveButton;
    public TMP_Text waveText;
    public Enemy[] Enemies;
    public Transform EnemiesParent;
    public Transform[] InstancePosition;
    public float Frequency;
    public int amountToSpawn;

    public GameObject winScreen;
    public GameObject upgradePanel;
    public GameObject settingsPanel;

    private float Timer;
    private int Position = 0;
    [HideInInspector] public bool isWaveActive = false;
    private int enemiesToSpawn = 5;
    private int currentEnemies;
    private int lastWave;

    public static int currentLevel = 1;

    [HideInInspector] public int currentWave = 0;
    public static GameManager gameManager { get; private set; }

    private void Awake()
    {
        gameManager = this;
    }

    private void Start()
    {
        if (!Application.isEditor)
            CrazySDK.Game.GameplayStart();
        if (currentLevel == 1)
        {
            lastWave = 30;
        }
        else if (currentLevel == 2)
        {
            lastWave = 40;
        }
        else if (currentLevel == 3)
        {
            lastWave = 50;
        }
        audioManager.PlayPrepTimeMusic();
        CloseUpgradePanel();
    }

    public void OpenUpgradePanel(Troop troop)
    {
        upgradePanel.SetActive(true);
        upgradeManager.SetTroop(troop);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        upgradeManager.active = false;
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        CloseUpgradePanel();
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void NextWave()
    {
        waveText.color = Color.white;
        nextWaveButton.SetActive(false);
        HandleWaveScaling();
    }

    public void HandleWaveEnd()
    {
        if (isWaveActive && currentEnemies == 0)
        {
            nextWaveButton.SetActive(true);
            isWaveActive = false;
            audioManager.PlayWaveClearMusic();
            audioManager.PlayPrepTimeMusic();
            waveText.text = "PREPARE FOR WAVE" + (currentWave + 1);
            GeneralResourceController.Instance.ClassifyResource(GeneralResourceController.ResourceType.Gold, 10);
            if (currentWave == 10 || currentWave == 20 || currentWave == 30)
            {
                if (!Application.isEditor)
                    CrazySDK.Game.HappyTime();
            }
        }
        if (currentWave == lastWave)
        {
            HandleWin();
        }
    }

    void HandleWaveScaling()
    {
        if (currentLevel == 1) { HandleWaveScalingLevel1(); }
        else if (currentLevel == 2) { HandleWaveScalingLevel2(); }
        else if (currentLevel == 3) { HandleWaveScalingLevel3(); }
    }

    private void HandleWaveScalingLevel3()
    {
        throw new System.NotImplementedException();
    }

    private void HandleWaveScalingLevel2()
    {
        throw new System.NotImplementedException();
    }

    private void HandleWaveScalingLevel1()
    {
        if (currentWave == 0)
        {
            currentWave++;
            enemiesToSpawn = amountToSpawn;
            currentEnemies = enemiesToSpawn;
            audioManager.PlayWaveStartMusic();
            waveText.text = "WAVE " + currentWave;
        }
        else
        {
            currentWave++;
            enemiesToSpawn = amountToSpawn + (2 * (currentWave-1));
            currentEnemies = enemiesToSpawn;
            Frequency -= 1f;
            audioManager.PlayWaveStartMusic();
            if (currentWave == 10 || currentWave == 20 || currentWave == 30)
            {
                audioManager.PlayBossMusic();
                waveText.color = Color.red;
                waveText.text = "BOSS WAVE";
            }
            else
            {
                audioManager.PlayCombatMusic();
                waveText.text = "WAVE " + currentWave;
            }
        }
        isWaveActive = true;
        Debug.Log("Wave " + currentWave + " has started!");
    }

    void FirstWaveSetOn()
    {
        isWaveActive = true;
        waveText.text = "WAVE 1";
        audioManager.PlayWaveStartMusic();
        audioManager.PlayCombatMusic();
        Debug.Log("Wave " + currentWave + " has started!");
    }

    private void HandleWin()
    {
        winScreen.SetActive(true);
        audioManager.PlayWinMusic();
    }

    private void Update()
    {
        HandleEnemySpawns();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUpgradePanel();
        }
    }

    void HandleEnemySpawns()
    {
        if (currentLevel == 1) { HandleEnemySpawnsLevel1(); }
        else if (currentLevel == 2) { HandleEnemySpawnsLevel2(); }
        else if (currentLevel == 3) { HandleEnemySpawnsLevel3(); }
    }
    private void HandleEnemySpawnsLevel3()
    {
        throw new System.NotImplementedException();
    }

    private void HandleEnemySpawnsLevel2()
    {
        throw new System.NotImplementedException();
    }

    private void HandleEnemySpawnsLevel1()
    {
        if (isWaveActive && Timer >= Frequency)
        {
            Position = Random.Range(0, InstancePosition.Length);
            int enemyIndex = -1;
            if (currentWave <= 2)
            {
                enemyIndex = 0;
            }
            else if (currentWave <= 5)
            {
                enemyIndex = Random.Range(0, 2);
            }
            else
            {
                enemyIndex = Random.Range(0, Enemies.Length);
            }
            int levelToSet = -1;
            if (currentWave <= 2)
            {
                levelToSet = 1;
            }
            else if (currentWave <= 5)
            {
                levelToSet = Random.Range(1, 3);
            }
            else if (currentWave < 10)
            {
                levelToSet = Random.Range(2, 4);
            }
            else if (currentWave < 15)
            {
                levelToSet = Random.Range(3, 5);
            }
            else if (currentWave < 20)
            {
                levelToSet = Random.Range(4, 6);
            }
            else if (currentWave < 25)
            {
                levelToSet = Random.Range(5, 7);
            }
            else if (currentWave < 30)
            {
                levelToSet = Random.Range(6, 8);
            }
            else
            {
                levelToSet = Random.Range(7, 9);
            }
            Enemy enemy = Instantiate(Enemies[enemyIndex], InstancePosition[Position].position, Quaternion.identity, EnemiesParent);
            enemy.SetLevel(levelToSet);
            Timer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isWaveActive)
            Timer += Time.deltaTime;
    }

    public void AddEnemy()
    {
        currentEnemies++;
    }

    public void RemoveEnemy()
    {
        currentEnemies--;
        HandleWaveEnd();
    }
}
