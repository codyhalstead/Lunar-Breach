using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class BaseEnemy : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;
    protected Animator animator;
    protected float animatorSpeed = 1f;

    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected AudioSource audioSource;
    protected Color originalColor;
    protected Animator anim;
    protected bool tookDamage = false;
    protected bool isDead = false;
    protected bool isFrozen = false;
    protected Color freezeColor = new Color(0.0235f, 1f, 1f, 1f);

    [Header("Stats")]
    public int maxHealth = 1000;
    public int currentHealth;
    public int currencyDrop = 100;

    [Header("Detection")]
    public Transform target;
    public float detectionRange = 10f;
    public float shootingRange = 5f;
    public float alertDuration = 1.5f;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform[] patrolPoints;
    protected int currentPatrolIndex = 0;

    [Header("Weapon")]
    public float shootCooldown = 1f;
    public float shotDamage = 1f;
    public float shotKnockback = 1f;
    public float shotSpeed = 1f;
    public float bulletLifeTime = 1f;

    protected float lastShootTime = 0f;
    protected bool hasBeenAlerted = false;
    protected Rigidbody2D rigidBody;


    public enum EnemyState
    {
        Idle,
        Patrolling,
        Alerted,
        Pursuing,
        Shooting,
        AltShooting,
        Fleeing
    }

    protected virtual void Start()
    {
        // Set enemy health, reference RB
        currentHealth = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
        // Set target to player if not assigned
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animatorSpeed = animator.speed; 
        }
    }

    public void Awake()
    {
        // Store sprite default color, reference animator
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        anim = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int amount)
    {
        // Subtract given damage amount from health, track that enemy was attacked 
        currentHealth -= amount;
        tookDamage = true;
        // Either die if hp now <= 0, or have player sprite flash red
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            if (spriteRenderer != null)
            {
                StartCoroutine(FlashColor(Color.red));
            }
        }
    }

    public virtual void Freeze(float duration)
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeRoutine(duration));
        }
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        spriteRenderer.color = freezeColor;
        isFrozen = true;
        if (animator != null)
        {
            animator.speed = 0;
        }
        yield return new WaitForSeconds(duration); 
        isFrozen = false;
        spriteRenderer.color = originalColor;
        if (animator != null)
        {
            animator.speed = animatorSpeed;
        }
    }

    private IEnumerator FlashColor(Color flashColor)
    {
        // Make enemy sprite flash given sprite color
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(0.1f);
        if (isFrozen)
        {
            spriteRenderer.color = freezeColor;
        } 
        else
        {
            spriteRenderer.color = originalColor;
        }
    }

    // Required method for all enemies
    public abstract void Die();

    public void giveCurrencyToPlayerTarget()
    {
        if (target != null && target.CompareTag("Player"))
        {
            PlayerCurrency playerCurrency = target.GetComponent<PlayerCurrency>();
            if (playerCurrency != null)
            {
                playerCurrency.AddCurrency(currencyDrop);
            }
        }
    }
}
