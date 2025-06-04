using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. HP left: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // You can play animation, drop loot, etc.
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }
}
