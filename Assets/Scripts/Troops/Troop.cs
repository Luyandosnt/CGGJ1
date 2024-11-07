using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class Troop : MonoBehaviour
{
    public int troopCost = 10;
    public float health = 5f; // Health of the troop
    public LayerMask enemyLayer; // Layer mask to detect enemies
    public float damage = 2; // Damage dealt to enemies
    public float attackRange = 1f; // Attack range of the troop
    public float attackFireRate = 1f; // Attacks per second
    public Animator animator;
    public GameObject deathObj;


    [Header("Variant Settings")]
    public GameObject fireObj;
    public float fireDPS = 1f;
    public float poisonDPS = 1f;
    public float slowPercentage = 0.3f;
    public float lifeStealPercentage = 0.1f;

    [Header("Leveling Up Settings")]
    public int level = 1;
    public int maxLevel = 5;
    public float attackRangeIncrement = 0.25f;
    public int healthIncrement = 2;
    public int damageIncrement = 1;
    public float fireDPSIncrement = 0.5f;
    public float poisonDPSIncrement = 0.5f;
    public float slowPercentageIncrement = 0.05f;
    public float lifeStealPercentageIncrement = 0.05f;


    [HideInInspector] public Variant variant = Variant.Base;
    [HideInInspector] public bool canLifesteal = false;

    protected float maxHealth; // Max Health of the troop
    protected float attackCooldown = 0f; // Tracks when the troop can attack again


    private void Start()
    {
        maxHealth = health;
    }
    void Update()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            Attack();
        }
        ProduceCoins();
    }

    protected abstract void ProduceCoins();
    protected abstract void Attack(); // Abstract attack method for melee/ranged variations
    protected abstract void DoDamage(); // Abstract attack method for melee/ranged variations

    public virtual void Heal(float percentage)
    {
        health += (int)(maxHealth * percentage);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void PerformDamage()
    {
        DoDamage();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! Health remaining: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        GetComponentInParent<Cell>().occupied = false;
        Instantiate(deathObj, animator.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected virtual void LevelUp()
    {
        level++;
        health += healthIncrement;
        maxHealth += healthIncrement;
        damage += damageIncrement;
        attackRange += attackRangeIncrement;
        lifeStealPercentage += lifeStealPercentageIncrement;
        fireDPS += fireDPSIncrement;
        poisonDPS += poisonDPSIncrement;
        slowPercentage += slowPercentageIncrement;
        Debug.Log($"{gameObject.name} has leveled up to level {level}!");
    }

    public enum Variant
    {
        Base,
        Flame,
        Frost,
        Venom
    }

    public void SetVariant(Variant variant)
    {
        this.variant = variant;
    }
}