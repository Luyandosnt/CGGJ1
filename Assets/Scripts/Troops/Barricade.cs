using UnityEngine;

public class Barricade : Troop
{
    // Override the Attack method to do nothing
    protected override void Attack()
    {
        // Barricade has no attack ability, so this is intentionally left empty
    }

    public override void TakeDamage(int damage)
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

    protected override void DoDamage() { }
}
