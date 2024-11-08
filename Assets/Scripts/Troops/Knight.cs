using UnityEngine;

public class Knight : Troop
{
    Enemy enemy;
    public RuntimeAnimatorController[] allAnimators;
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
            float healAmount = Mathf.Min((enemy.maxHealth * lifeStealPercentage), health - maxHealth);
            health += healAmount;
            Debug.Log($"{gameObject.name} healed for {healAmount} health! Health remaining: {health}");
        }
        switch (variant)
        {
            case Variant.Base:
                break;
            case Variant.Flame:
                Instantiate(fireObj, enemy.transform.position, Quaternion.identity);
                break;
            case Variant.Frost:
                break;
        }
    }

    protected override void SetElement(Variant variant)
    {
        if (!canLifesteal)
        {
            if (variant == Variant.Flame)
            {
                troopName = "Flame Knight";
                animator.runtimeAnimatorController = allAnimators[1];
            }
            else if (variant == Variant.Frost)
            {
                troopName = "Frost Knight";
                animator.runtimeAnimatorController = allAnimators[2];
            }
            else if (variant == Variant.Venom)
            {
                troopName = "Venom Knight";
                animator.runtimeAnimatorController = allAnimators[3];
            }
        }
        else
        {
            if (variant == Variant.Base)
            {
                troopName = "Arcane Knight";
                animator.runtimeAnimatorController = allAnimators[0];
            }
            else if (variant == Variant.Flame)
            {
                troopName = "Arcane Flame Knight";
                animator.runtimeAnimatorController = allAnimators[1];
            }
            else if (variant == Variant.Frost)
            {
                troopName = "Arcane Frost Knight";
                animator.runtimeAnimatorController = allAnimators[2];
            }
            else if (variant == Variant.Venom)
            {
                troopName = "Arcane Venom Knight";
                animator.runtimeAnimatorController = allAnimators[3];
            }
        }
    }

    protected override void ProduceCoins() { }

    // Draw attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
