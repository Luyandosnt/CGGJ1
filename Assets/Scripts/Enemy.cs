using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform HealthBar;
    public LayerMask troopLayer; // Layer mask to detect troops

    [Header("Enemy Stats")]
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float attackFireRate = 1f; // Attacks per second
    public float damage = 1; // Damage dealt to troops
    public float health = 5; // Health of the enemy
    public int level = 1;

    [Header("Coin Loot")]
    public GameObject Coin;
    public Vector2 coinsMinMaxAmount;

    [Header("Resource Loots")]
    public GameObject ResourceLoots;
    [Range(0, 100)] public float resourcesLootChances;
    public Vector2 resourcesMinMaxAmount;

    //Private Variables
    private float attackCooldown = 0f; // Tracks when the enemy can attack again
    private Transform targetTroop; // Reference to the target troop being attacked
    private bool isAttacking = false; // Flag to check if the enemy is attacking
    [HideInInspector] public float maxHealth;
    [HideInInspector] public bool lifeStolen = false;

    //This method must be called before this enemy attacks or is attacked
    public void SetLevel(int level)
    {
        this.level = level;
        health = health + (level - 1) * 2;
        maxHealth = health;
        damage = damage + (level - 1);
    }

    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        if (!isAttacking)
            // Move left continuously
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        // Perform a raycast to check for troops in attack range
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackRange, troopLayer);
        if (hit.collider != null && hit.collider.CompareTag("Troop"))
        {
            Debug.Log($"{gameObject.name} is attacking {hit.collider.name}!");
            isAttacking = true; // Set attacking flag to true
            targetTroop = hit.transform; // Set the target troop
            if (attackCooldown <= 0f)
            {
                // Attack the troop
                hit.collider.GetComponent<Troop>().TakeDamage(damage);
                attackCooldown = 1f / attackFireRate; // Reset cooldown based on fire rate
            }
        }
        else
        {
            Debug.Log($"{gameObject.name} is searching for a target...");
            targetTroop = null; // No target in range
            isAttacking = false; // Set attacking flag to false
        }

        // Cooldown logic
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage, bool Weakness)
    {
        if(!Weakness)
        {
            health -= damage; // Reduce health
            Debug.Log($"{gameObject.name} took {damage} damage! Health remaining: {health}");

            if (health <= 0)
            {
                Die(); // Call die function if health is zero or less
            }
        }
        else
        {
            health -= damage * 2; // Reduce health
            Debug.Log($"{gameObject.name} took {damage} damage! Health remaining: {health}");

            if (health <= 0)
            {
                Die(); // Call die function if health is zero or less
            }
        }
        HealthBar.localScale = new Vector3(health / maxHealth, HealthBar.localScale.y, HealthBar.localScale.z);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        // Implement defeat logic here (e.g., play death animation, destroy object)
        GameManager.gameManager.RemoveEnemy();
        DropLoot();
        Destroy(gameObject); // Example: destroy the enemy game object
    }

    private void OnDrawGizmos()
    {
        // Draw a line to visualize the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * attackRange);
    }
    
    private void DropLoot()
    {
        // Drop coins
        int coinsAmount = Random.Range((int)coinsMinMaxAmount.x, (int)coinsMinMaxAmount.y);
        for (int i = 0; i < coinsAmount; i++)
        {
            Instantiate(Coin, transform.position, transform.rotation);
        }
        // Drop resources
        if (Random.Range(0f, 100f) < resourcesLootChances)
        {
            int resourcesAmount = Random.Range((int)resourcesMinMaxAmount.x, (int)resourcesMinMaxAmount.y+1);
            for (int i = 0; i < resourcesAmount; i++)
            {
                //Make Random rotation
                Quaternion quaternion = Quaternion.Euler(0, 0, Random.Range(0, 360));
                Instantiate(ResourceLoots, transform.position, quaternion);
            }
        }
    }
}
