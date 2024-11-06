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
            animator.SetBool("Attack", true);
            Invoke("DoDamage", animToAttackTime);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    public void DoDamage()
    {
        if (enemy != null)
            enemy.TakeDamage(damage, false);
        attackCooldown = 1f / attackFireRate; // Reset cooldown
    }

    // Draw attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
