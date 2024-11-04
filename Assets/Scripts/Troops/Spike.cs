using UnityEngine;

public class Spike : MonoBehaviour
{
    public int maxUses = 3;
    public int health = 3; // Initial health with 3 uses
    public int damage = 5; // Initial health with 3 uses
    public float cooldownTime = 15f; // Time to reactivate after triggering
    private bool isActivated = true; // Indicates if the trap is currently active
    private float cooldownTimer = 0f;

    private void Update()
    {
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
        if (isActivated && other.CompareTag("Enemy"))
        {
            ActivateTrap(other.gameObject);
        }
    }

    private void ActivateTrap(GameObject enemy)
    {
        Debug.Log($"{gameObject.name} triggered by {enemy.name}!");

        // Deal damage to enemy (if needed)
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage(damage); // Adjust damage as needed
        }

        health--; // Reduce Spike health
        isActivated = false; // Deactivate temporarily
        cooldownTimer = cooldownTime; // Start cooldown

        if (health <= 0)
        {
            DestroyTrap(); // Destroy when health reaches 0
        }
    }

    private void Reactivate()
    {
        isActivated = true;
        Debug.Log($"{gameObject.name} is reactivated and ready to trigger again.");
    }

    private void DestroyTrap()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");
        Destroy(gameObject);
    }
}
