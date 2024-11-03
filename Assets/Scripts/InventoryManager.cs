using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [Header("Resources")]
    private int essenceCrystal = 0;
    private int gold = 0;
    private int infernoEmber = 0;
    private int frozenShard = 0;
    private int venomGland = 0;

    [Header("Runes")]
    private int fireRune;
    private int frostRune;
    private int magicRune;
    private int poisonRune;

    [Header("Potions")]
    private int poisonPotion;
    private int frostPotion;
    private int firePotion;
    private int healthPotion;

    #region - Base resources -
    public void DecreaseResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.EssenceCrystal:
                essenceCrystal -= amount;
                break;
            case ResourceType.Gold:
                gold++;
                break;
            case ResourceType.InfernoEmber:
                infernoEmber -= amount;
                break;
            case ResourceType.FrozenShard:
                frozenShard -= amount;
                break;
            case ResourceType.VenomGland:
                venomGland -= amount;
                break;
        }
    }

    public void ClassifyResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.EssenceCrystal:
                essenceCrystal++;
                break;
            case ResourceType.Gold:
                gold++;
                break;
            case ResourceType.InfernoEmber:
                infernoEmber++;
                break;
            case ResourceType.FrozenShard:
                frozenShard++;
                break;
            case ResourceType.VenomGland:
                venomGland++;
                break;
        }
        ShowStorage();
    }

    #endregion




    #region - Runes -

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
        print("///////////////////////////////////");
        print("EssenceCrystal " + essenceCrystal);
        print("Gold " + gold);
        print("InfernoEmber " + infernoEmber);
        print("FrozenShard " + frozenShard);
        print("VenomGland " + venomGland);
        print("///////////////////////////////////");
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
