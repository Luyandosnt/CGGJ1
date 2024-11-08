using TMPro;
using UnityEngine;

public class GeneralResourceController : MonoBehaviour
{
    public static GeneralResourceController Instance { get; private set; }
    public Transform ghostContainer;
    public Transform[] ghostResourceDestinations;
    public TroopPlacement troopPlacement;

    public TMP_Text[] resourceTexts;
    public TMP_Text[] runesTexts;

    public GameObject resourcePanel;

    // Resources
    private int gold = 0;
    public int[] resources;

    // Runes
    public int[] Runes;
    public int runeIndex = -1;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeResources();
    }

    public void ToggleResourcePanel()
    {
        resourcePanel.SetActive(!resourcePanel.activeSelf);
    }

    private void Update()
    {
        runesTexts[0].text = Runes[0].ToString();
        runesTexts[1].text = Runes[1].ToString();
        runesTexts[2].text = Runes[2].ToString();
        runesTexts[3].text = Runes[3].ToString();
    }

    public void SetRuneIndex(int index)
    {
        runeIndex = index;
        troopPlacement.currentTroopIndex = -1;
    }

    public void InitializeResources()
    {
        gold = 20;
        resources[0] = 0;
        resources[1] = 0;
        resources[2] = 0;
        resources[3] = 0;
        resourceTexts[0].text = gold.ToString();
        resourceTexts[1].text = resources[0].ToString();
        resourceTexts[2].text = resources[1].ToString();
        resourceTexts[3].text = resources[2].ToString();
        resourceTexts[4].text = resources[3].ToString();
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
                resources[0] += amount;
                resourceTexts[1].text = resources[0].ToString();
                break;
            case ResourceType.FrozenShard:
                resources[1] += amount;
                resourceTexts[2].text = resources[1].ToString();
                break;
            case ResourceType.VenomGland:
                resources[2] += amount;
                resourceTexts[3].text = resources[2].ToString();
                break;
            case ResourceType.EssenceCrystal:
                resources[3] += amount;
                resourceTexts[4].text = resources[3].ToString();
                break;
        }
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
                resources[0] -= amount;
                resourceTexts[1].text = resources[0].ToString();
                break;
            case ResourceType.FrozenShard:
                resources[1] -= amount;
                resourceTexts[2].text = resources[1].ToString();
                break;
            case ResourceType.VenomGland:
                resources[2] -= amount;
                resourceTexts[3].text = resources[2].ToString();
                break;
            case ResourceType.EssenceCrystal:
                resources[3] -= amount;
                resourceTexts[4].text = resources[3].ToString();
                break;
        }
    }

    public int GetResourceIndex(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Gold:
                return 0;
            case ResourceType.InfernoEmber:
                return 1;
            case ResourceType.FrozenShard:
                return 2;
            case ResourceType.VenomGland:
                return 3;
            case ResourceType.EssenceCrystal:
                return 4;
            default:
                return 0;
        }
    }
    #endregion

    public void CraftRune(int index)
    {
        CraftRunes((RuneType)index);
    }

    #region - Runes -
    public void ClassifyRunes(RuneType type)
    {
        switch (type)
        {
            case RuneType.FireRune:
                Runes[0]++;
                break;
            case RuneType.FrostRune:
                Runes[1]++;
                break;
            case RuneType.PoisonRune:
                Runes[2]++;
                break;
            case RuneType.MagicRune:
                Runes[3]++;
                break;
        }
    }

    public void DecreaseRunes(RuneType type, int amount)
    {
        switch (type)
        {
            case RuneType.FireRune:
                Runes[0] -= amount;
                break;
            case RuneType.FrostRune:
                Runes[1] -= amount;
                break;
            case RuneType.MagicRune:
                Runes[3] -= amount;
                break;
            case RuneType.PoisonRune:
                Runes[2] -= amount;
                break;
        }
    }

    public void CraftRunes(RuneType type)
    {
        switch (type)
        {
            case RuneType.FireRune:
                if (resources[0] >= 3)
                {
                    resources[0] -= 3;
                    Runes[0]++;
                }
                break;
            case RuneType.FrostRune:
                if (resources[1] >= 3)
                {
                    resources[1] -= 3;
                    Runes[1]++;
                }
                break;
            case RuneType.MagicRune:
                if (resources[3] >= 3)
                {
                    resources[3] -= 3;
                    Runes[3]++;
                }
                break;
            case RuneType.PoisonRune:
                if (resources[2] >= 3)
                {
                    resources[2] -= 3;
                    Runes[2]++;
                }
                break;
        }
    }

    #endregion

    public int GetResourceAmount(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Gold:
                return gold;
            case ResourceType.InfernoEmber:
                return resources[0];
            case ResourceType.FrozenShard:
                return resources[1];
            case ResourceType.VenomGland:
                return resources[2];
            case ResourceType.EssenceCrystal:
                return resources[3];
            default:
                return 0;
        }
    }

    public enum ResourceType
    {
        Gold,
        InfernoEmber,
        FrozenShard,
        VenomGland,
        EssenceCrystal
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
}
