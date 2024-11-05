using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] Loots;
    public Image HealthBar;
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float attackFireRate = 1f; // Attacks per second
    public int damage = 1; // Damage dealt to troops
    public LayerMask troopLayer; // Layer mask to detect troops
    public int health = 5; // Health of the enemy

    private int Chances;
    private float attackCooldown = 0f; // Tracks when the enemy can attack again
    private Transform targetTroop; // Reference to the target troop being attacked
    private bool isAttacking = false; // Flag to check if the enemy is attacking


    void Start()
    {
        Chances = GameManager.gameManager.LootChances;
    }

    void Update()
    {
        Move();
        Attack();
        HealthBar.fillAmount = health * 0.2f;
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

    public void TakeDamage(int damage, bool Weakness)
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
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        // Implement defeat logic here (e.g., play death animation, destroy object)
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
        int Chance = Random.Range(0, Chances);
        if(Chance <= Chances)
        {
            Instantiate(Loot[Random.Range(0, Loots.Length)], transform.position, transform.rotation);
        }
    }
}
