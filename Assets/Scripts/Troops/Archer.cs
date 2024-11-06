using UnityEngine;

public class Archer : Troop
{
    public GameObject arrowPrefab; // Prefab of the arrow projectile

    protected override void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, enemyLayer);
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log($"{gameObject.name} is shooting at {hit.collider.name}!");
            SpawnArrow(hit.collider.transform.position);
            attackCooldown = 1f / attackFireRate; // Reset cooldown
        }
    }

    private void SpawnArrow(Vector3 targetPosition)
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetTarget(targetPosition, damage);
    }

    protected override void DoDamage() { }

    // Draw attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
