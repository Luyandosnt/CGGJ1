using UnityEngine;

public class Troop : MonoBehaviour
{
    public int health = 5; // Health of the troop
    public LayerMask enemyLayer; // Layer mask to detect enemies
    public int damage = 2; // Damage dealt to enemies
    public float attackRange = 1f; // Attack range of the troop
    public float attackFireRate = 1f; // Attacks per second
    private float attackCooldown = 0f; // Tracks when the troop can attack again

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        // Perform a raycast to check for enemies in attack range
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, enemyLayer);
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log($"{gameObject.name} is attacking {hit.collider.name}!");
            if (attackCooldown <= 0f)
            {
                // Attack the enemy
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                attackCooldown = 1f / attackFireRate; // Reset cooldown based on fire rate
            }
        }

        // Cooldown logic
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce health
        Debug.Log($"{gameObject.name} took {damage} damage! Health remaining: {health}");

        if (health <= 0)
        {
            Die(); // Call die function if health is zero or less
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        // Implement defeat logic here (e.g., play death animation, destroy object)
        Destroy(gameObject); // Example: destroy the troop game object
    }

    private void OnDrawGizmos()
    {
        // Draw a line to visualize the attack range
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * attackRange);
    }
}
