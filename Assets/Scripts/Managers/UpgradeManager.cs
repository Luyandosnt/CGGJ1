using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public TMP_Text troopName;
    public TMP_Text troopLevel;
    public TMP_Text troopUpgradeCost;
    public TMP_Text troopCurrentHealth;
    public TMP_Text troopUpgradeHealth;
    public TMP_Text troopCurrentDamage;
    public TMP_Text troopUpgradeDamage;
    public TMP_Text elementCurrentDamage;
    public TMP_Text elementUpgradeDamage;
    public TMP_Text lifestealCurrentAmount;
    public TMP_Text lifestealUpgradeAmount;
    public Image coloredElementIndicator;
    public Button upgradeButton;
    public Color[] elementalColors;

    private Troop troop;

    [HideInInspector] public bool active = false;

    private void Update()
    {
        if (active)
        {
            if (troop.troopUpgradeCost > GeneralResourceController.Instance.GetResourceAmount(GeneralResourceController.ResourceType.Gold))
            {
                upgradeButton.interactable = false;
                troopUpgradeCost.color = Color.red;
            }
            else
            {
                upgradeButton.interactable = true;
                troopUpgradeCost.color = Color.white;
            }
        }
    }


    public void SetTroop(Troop _troop)
    {
        active = true;
        troop = _troop;
        troopName.text = troop.troopName;
        troopLevel.text = "Level " + troop.level;
        troopUpgradeCost.text = troop.troopCost.ToString();
        troopCurrentHealth.text = "Max Health: " + troop.maxHealth;
        troopUpgradeHealth.text = "+" + troop.healthIncrement;
        troopCurrentDamage.text = "Damage: " + troop.damage;
        troopUpgradeDamage.text = "+" + troop.damageIncrement;
        if (troop.variant == Troop.Variant.Base)
        {
            coloredElementIndicator.color = Color.white;
            elementCurrentDamage.text = "";
            elementUpgradeDamage.text = "";
            elementCurrentDamage.gameObject.SetActive(false);
        }
        else if (troop.variant == Troop.Variant.Flame)
        {
            elementCurrentDamage.gameObject.SetActive(true);
            coloredElementIndicator.color = elementalColors[0];
            elementCurrentDamage.text = "Fire DPS: " + troop.fireDPS;
            elementUpgradeDamage.text = "+" + troop.fireDPSIncrement;
        }
        else if (troop.variant == Troop.Variant.Frost)
        {
            elementCurrentDamage.gameObject.SetActive(true);
            coloredElementIndicator.color = elementalColors[1];
            elementCurrentDamage.text = "Slowness Percent: " + troop.slowPercentage;
            elementUpgradeDamage.text = "+" + troop.slowPercentageIncrement;
        }
        else if (troop.variant == Troop.Variant.Venom)
        {
            elementCurrentDamage.gameObject.SetActive(true);
            coloredElementIndicator.color = elementalColors[2];
            elementCurrentDamage.text = "Poison DPS: " + troop.poisonDPS;
            elementUpgradeDamage.text = "+" + troop.poisonDPSIncrement;
        }
        if (troop.canLifesteal)
        {
            lifestealCurrentAmount.gameObject.SetActive(true);
            coloredElementIndicator.color = elementalColors[3];
            lifestealCurrentAmount.text = "Lifesteal Percent: " + troop.lifeStealPercentage;
            lifestealUpgradeAmount.text = "+" + troop.lifeStealPercentageIncrement + "%";
        }
        else
        {
            lifestealCurrentAmount.text = "";
            lifestealUpgradeAmount.text = "";
            lifestealCurrentAmount.gameObject.SetActive(false);
        }
        Barricade barricade = troop.GetComponent<Barricade>();
        if (barricade)
        {
            lifestealCurrentAmount.gameObject.SetActive(true);
            lifestealCurrentAmount.text = "Coin Interval: " + barricade._coinProductionInterval;
            lifestealUpgradeAmount.text = "-" + barricade.coinProductionIntervalIncrement + "s";
        }
        if (troop.level >= troop.maxLevel)
        {
            upgradeButton.interactable = false;
            troopUpgradeCost.text = "Max Level";
            troopUpgradeCost.color = Color.gray;
            troopUpgradeHealth.text = "";
            troopUpgradeDamage.text = "";
            elementUpgradeDamage.text = "";
            lifestealUpgradeAmount.text = "";
        }
        if (troop.troopUpgradeCost > GeneralResourceController.Instance.GetResourceAmount(GeneralResourceController.ResourceType.Gold))
        {
            upgradeButton.interactable = false;
            troopUpgradeCost.color = Color.red;
        }
        else
        {
            upgradeButton.interactable = true;
            troopUpgradeCost.color = Color.white;
        }
    }

    public void UpgradeTroop()
    {
        if (troop != null && troop.level < troop.maxLevel && GeneralResourceController.Instance.GetResourceAmount(GeneralResourceController.ResourceType.Gold) >= troop.troopUpgradeCost)
        {
            if (troop.GetComponent<Barricade>() != null)
            {
                troop.GetComponent<Barricade>().LevelUp();
            }
            else
            { troop.LevelUp(); }
            SetTroop(troop);
        }
    }
}
