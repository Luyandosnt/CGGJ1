using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 5f;
    private int damage;
    private Vector3 targetPosition;

    public Archer owner;
    public Enemy enemy;

    public void SetTarget(Vector3 target, int damageAmount)
    {
        targetPosition = target;
        damage = damageAmount;
    }

    void Update()
    {
        // Move the arrow towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Destroy the arrow when it reaches the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage, false);
            owner.Heal(enemy.health);
            Destroy(gameObject); // Destroy the arrow on impact
        }
    }
}
