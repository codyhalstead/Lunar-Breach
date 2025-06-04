using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private HashSet<GameObject> hitEnemies = new();

    [Header("Damage Settings")]
    public int damage = 10;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;

    public bool destroyOnImpact = true;
    public bool noMultiHits = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy1 enemy = collision.gameObject.GetComponent<Enemy1>();
        if (enemy != null)
        {
            HandleHit(collision.gameObject, transform.position);
        }

        Destroy(gameObject);
    }

    // If using triggers:
    void OnTriggerEnter2D(Collider2D other)
    {
        if (noMultiHits && hitEnemies.Contains(other.gameObject))
        {
            return;
        }

        HandleHit(other.gameObject, transform.position);

    }

    protected void HandleHit(GameObject target, Vector2 hitPoint)
    {
        Enemy1 enemy = target.GetComponent<Enemy1>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            //Debug.LogWarning("Enemy found!");
            // Optional knockback
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
       
            if (enemyRb != null)
            {
                //Debug.LogWarning("Enemy RB found");
                Vector2 direction = (Vector2)enemy.transform.position - hitPoint;
                direction.Normalize();
                enemyRb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
            hitEnemies.Add(target);
        }
        if (destroyOnImpact) {
            Destroy(gameObject);
        }
    }
}
