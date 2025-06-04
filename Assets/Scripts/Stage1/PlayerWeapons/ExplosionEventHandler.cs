using UnityEngine;

public class ExplosionEventHandler : MonoBehaviour
{
    public GameObject sourceGrenade;
    public int damage = 50;
    public float knockbackForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy1 enemy = other.GetComponent<Enemy1>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 direction = (Vector2)enemy.transform.position - (Vector2)transform.position;
                direction.Normalize();
                enemyRb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    public void OnExplosionAnimationEnd()
    {
        if (sourceGrenade != null)
        {
            Destroy(sourceGrenade);
        }

        Destroy(gameObject);
    }
}
