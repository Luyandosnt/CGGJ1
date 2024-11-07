using UnityEngine;

public class Knight : Troop
{
    Enemy enemy;
    protected override void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, enemyLayer);
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log($"{gameObject.name} is attacking {hit.collider.name}!");
            enemy = hit.collider.GetComponent<Enemy>();
            attackCooldown = 1f / attackFireRate; // Reset cooldown
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    protected override void DoDamage()
    {
        if (enemy != null)
            enemy.TakeDamage(damage, false);
        if (enemy == null || enemy.health <= 0)
            animator.SetBool("Attack", false);
        if (canLifesteal && enemy.health <= 0 && !enemy.lifeStolen)
        {
            // Heal the knight based on the percentage of the enemy's max health, to maximum of the knight's max health
            int healAmount = Mathf.Min((int)(enemy.maxHealth * lifeStealPercentage), health - maxHealth);
            health += healAmount;
            Debug.Log($"{gameObject.name} healed for {healAmount} health! Health remaining: {health}");
        }
    }

    // Draw attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
