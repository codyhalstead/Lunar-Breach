using Unity.VisualScripting;
using UnityEngine;


public class HomingProj : MonoBehaviour
{
    public AudioClip explosionSound;
    public AudioClip thrusterSound;
    [SerializeField] private AudioSource audioSource;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public float lifetime = 5f;
    public int damage;
    public float explosionDelay;
    public float knockback;
    public Transform target;
    public GameObject explosionEffect;

    private bool hasExploded = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Automatically find the player by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        if (explosionSound != null && audioSource != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        // Set auto explosion time
        Invoke(nameof(Explode), explosionDelay);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collision triggered
        if (!hasExploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;
        if (explosionEffect != null)
        {
            // Create explosion object
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1.5f);
            }
            // Pass on data to explosion handler (including this game object) if applicable
            ExplosionEventHandler handler = explosion.GetComponent<ExplosionEventHandler>();
            if (handler != null)
            {
                handler.damage = damage;
                handler.knockbackForce = knockback;
                handler.isTargetPlayer = true;
            }
        }
        // Destroy this grenade game object
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (target == null || rb == null) return;

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.right).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.linearVelocity = transform.right * speed;
    }
}