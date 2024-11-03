using TMPro;
using UnityEngine;

public class GeneralResourceController : MonoBehaviour
{
    public static GeneralResourceController Instance { get; private set; }
    public Transform ghostContainer;

    public TMP_Text[] resourceTexts;

    // Resources
    private int essenceCrystal = 0;
    private int gold = 0;
    private int infernoEmber = 0;
    private int frozenShard = 0;
    private int venomGland = 0;

    // Runes
    private int fireRune = 0;
    private int frostRune = 0;
    private int magicRune = 0;
    private int poisonRune = 0;

    // Potions
    private int poisonPotion = 0;
    private int frostPotion = 0;
    private int firePotion = 0;
    private int healthPotion = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make persistent
        }
    }

    private void Start()
    {
        InitializeResources();
    }

    public void InitializeResources()
    {
        gold = 20;
        infernoEmber = 0;
        frozenShard = 0;
        venomGland = 0;
        essenceCrystal = 0;
        resourceTexts[0].text = gold.ToString();
        resourceTexts[1].text = infernoEmber.ToString();
        resourceTexts[2].text = frozenShard.ToString();
        resourceTexts[3].text = venomGland.ToString();
        resourceTexts[4].text = essenceCrystal.ToString();
    }

    public void ResourceClicked(ResourceType type, int amount)
    {
        ClassifyResource(type, amount);
    }

    #region - Base Resources -
    public void ClassifyResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Gold:
                gold += amount;
                resourceTexts[0].text = gold.ToString();
                break;
            case ResourceType.InfernoEmber:
                infernoEmber += amount;
                resourceTexts[1].text = infernoEmber.ToString();
                break;
            case ResourceType.FrozenShard:
                frozenShard += amount;
                resourceTexts[2].text = frozenShard.ToString();
                break;
            case ResourceType.VenomGland:
                venomGland += amount;
                resourceTexts[3].text = venomGland.ToString();
                break;
            case ResourceType.EssenceCrystal:
                essenceCrystal += amount;
                resourceTexts[4].text = essenceCrystal.ToString();
                break;
        }
        ShowStorage();
    }

    public void DecreaseResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Gold:
                gold -= amount;
                resourceTexts[0].text = gold.ToString();
                break;
            case ResourceType.InfernoEmber:
                infernoEmber -= amount;
                resourceTexts[1].text = infernoEmber.ToString();
                break;
            case ResourceType.FrozenShard:
                frozenShard -= amount;
                resourceTexts[2].text = frozenShard.ToString();
                break;
            case ResourceType.VenomGland:
                venomGland -= amount;
                resourceTexts[3].text = venomGland.ToString();
                break;
            case ResourceType.EssenceCrystal:
                essenceCrystal -= amount;
                resourceTexts[4].text = essenceCrystal.ToString();
                break;
        }
    }
    #endregion

    #region - Runes -
    public void ClassifyRunes(RuneType type)
    {
        switch (type)
        {
            case RuneType.FireRune:
                fireRune++;
                break;
            case RuneType.FrostRune:
                frostRune++;
                break;
            case RuneType.MagicRune:
                magicRune++;
                break;
            case RuneType.PoisonRune:
                poisonRune++;
                break;
        }
    }

    public void DecreaseRunes(RuneType type)
    {
        switch (type)
        {
            case RuneType.FireRune:
                fireRune--;
                break;
            case RuneType.FrostRune:
                frostRune--;
                break;
            case RuneType.MagicRune:
                magicRune--;
                break;
            case RuneType.PoisonRune:
                poisonRune--;
                break;
        }
    }
    #endregion

    #region - Potions -
    public void ClassifyPotions(PotionType type)
    {
        switch (type)
        {
            case PotionType.PoisonPotion:
                poisonPotion++;
                break;
            case PotionType.FrostPotion:
                frostPotion++;
                break;
            case PotionType.FirePotion:
                firePotion++;
                break;
            case PotionType.HealthPotion:
                healthPotion++;
                break;
        }
    }

    public void DecreasePotions(PotionType type)
    {
        switch (type)
        {
            case PotionType.PoisonPotion:
                poisonPotion--;
                break;
            case PotionType.FrostPotion:
                frostPotion--;
                break;
            case PotionType.FirePotion:
                firePotion--;
                break;
            case PotionType.HealthPotion:
                healthPotion--;
                break;
        }
    }
    #endregion

    private void ShowStorage()
    {
        Debug.Log($"///////////////////////////////////\n" +
                  $"EssenceCrystal: {essenceCrystal}\n" +
                  $"Gold: {gold}\n" +
                  $"InfernoEmber: {infernoEmber}\n" +
                  $"FrozenShard: {frozenShard}\n" +
                  $"VenomGland: {venomGland}\n" +
                  $"///////////////////////////////////");
    }
}

public enum ResourceType
{
    EssenceCrystal,
    Gold,
    InfernoEmber,
    FrozenShard,
    VenomGland
}

public enum RuneType
{
    FireRune,
    FrostRune,
    MagicRune,
    PoisonRune
}

public enum PotionType
{
    PoisonPotion,
    FrostPotion,
    FirePotion,
    HealthPotion
}
