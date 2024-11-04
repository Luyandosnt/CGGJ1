using UnityEngine;

public class Spike : Troop
{
    public int maxUses = 3;
    public float cooldownTime = 15f; // Time to reactivate after triggering
    private bool isActivated = true; // Indicates if the trap is currently active
    private float cooldownTimer = 0f;

    private void Start()
    {
        health = maxUses; // Set health to max uses, so it can be used only `maxUses` times
        damage = 5; // Spike damage value
    }

    private void Update()
    {
        // Cooldown logic to reactivate the trap
        if (!isActivated)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                Reactivate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger the trap if an enemy steps on it and it is activated
        if (isActivated && other.CompareTag("Enemy"))
        {
            ActivateTrap(other.gameObject);
        }
    }

    private void ActivateTrap(GameObject enemy)
    {
        Debug.Log($"{gameObject.name} triggered by {enemy.name}!");

        // Deal damage to the enemy
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage(damage);
        }

        health--; // Reduce Spike's health (use count)
        isActivated = false; // Temporarily deactivate after activation
        cooldownTimer = cooldownTime; // Start cooldown timer

        if (health <= 0)
        {
            DestroyTrap(); // Destroy if out of uses
        }
    }

    private void Reactivate()
    {
        isActivated = true; // Reset trap to activated state
        Debug.Log($"{gameObject.name} is reactivated and ready to trigger again.");
    }

    private void DestroyTrap()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");
        GetComponentInParent<Cell>().occupied = false; // Mark the cell as unoccupied
        Destroy(gameObject);
    }

    // Override Attack to prevent Spike from attempting to "attack" actively
    protected override void Attack()
    {
        // Spike has no active attack functionality
    }

    public override void TakeDamage(int damage)
    {
        // Spikes don’t take external damage; this can be left empty or include custom logic if needed
    }
}
