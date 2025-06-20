using UnityEngine;

public class ExplosionEventHandler : MonoBehaviour
{
    public int damage = 50;
    public float knockbackForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Collision triggered
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            // Collision was an enemy, do damage
            enemy.TakeDamage(damage);
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                // Enemy has RB, apply knockback
                Vector2 direction = (Vector2)enemy.transform.position - (Vector2)transform.position;
                direction.Normalize();
                enemyRb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    public void OnExplosionAnimationEnd()
    {
        // Destroy the explosion object
        Destroy(gameObject);
    }
}
