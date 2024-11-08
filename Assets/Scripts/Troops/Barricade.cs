using UnityEngine;

public class Barricade : Troop
{
    [Header("Coin Properties")]
    public GameObject coin;
    public float coinProductionInterval = 5f;

    public float coinProductionIntervalIncrement = 0.5f;

    [HideInInspector] public float _coinProductionInterval;

    private void Start()
    {
        _coinProductionInterval = coinProductionInterval;
    }
    // Override the Attack method to do nothing
    protected override void Attack()
    {
        // Barricade has no attack ability, so this is intentionally left empty
    }

    protected override void ProduceCoins()
    {
        // Produce coins at a fixed rate
        if (GameManager.gameManager.isWaveActive)
        {
            coinProductionInterval -= Time.deltaTime;
            if (coinProductionInterval <= 0)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
                coinProductionInterval = _coinProductionInterval;
            }
        }
        else
        {
            coinProductionInterval = _coinProductionInterval;
        }
    }

    public override void TakeDamage(float damage)
    {
        // Call the base class's TakeDamage method to reduce health
        base.TakeDamage(damage);

        // Additional behavior specific to barricades (if needed) can be added here
        Debug.Log($"{gameObject.name} (Barricade) took {damage} damage! Health remaining: {health}");

        // Check if health is depleted
        if (health <= 0)
        {
            Die(); // Call die function if health is zero or less
        }
    }

    protected override void SetElement(Variant variant){}

    public override void LevelUp()
    {
        base.LevelUp();
        _coinProductionInterval -= coinProductionIntervalIncrement;
    }

    protected override void DoDamage() { }
}
