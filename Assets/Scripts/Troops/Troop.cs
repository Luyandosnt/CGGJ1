using UnityEngine;

public abstract class Troop : MonoBehaviour
{
    public int troopCost = 10;
    public int health = 5; // Health of the troop
    protected int maxHealth; // Health of the troop
    public bool canLifesteal;
    public float lifeStealPercentage = 0.1f;
    public float animToAttackTime = 0f; // Time to transition from animation to attack
    public LayerMask enemyLayer; // Layer mask to detect enemies
    public int damage = 2; // Damage dealt to enemies
    public float attackRange = 1f; // Attack range of the troop
    public float attackFireRate = 1f; // Attacks per second
    protected float attackCooldown = 0f; // Tracks when the troop can attack again
    public Animator animator;

    bool doLifesteal = false;

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
    }

    protected abstract void Attack(); // Abstract attack method for melee/ranged variations
    protected abstract void DoDamage(); // Abstract attack method for melee/ranged variations

    public virtual void PerformDamage()
    {
        DoDamage();
    }

    public virtual void TakeDamage(int damage)
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
        Destroy(gameObject);
    }
}
