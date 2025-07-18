using System;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCollision : MonoBehaviour
{
    public AudioClip explosionSound;
    public AudioClip thrusterSound;
    [SerializeField] private AudioSource audioSource;
    public float explosionDelay = 1f;
    public float explosionRadius = 2f;
    public int damage = 50;
    public float knockbackForce = 10f;
    public GameObject explosionEffect;
    public bool isFreezing = false;
    public float freezeDuration = 0f;

    private bool hasExploded = false;

    void Start()
    {
        if (audioSource != null && thrusterSound != null)
        {
            audioSource.clip = thrusterSound;
            audioSource.loop = true;
            audioSource.Play();
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
            if (explosionSound != null && audioSource != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1f);
            }
            // Pass on data to explosion handler (including this game object) if applicable
            ExplosionEventHandler handler = explosion.GetComponent<ExplosionEventHandler>();
            if (handler != null)
            {
                handler.damage = damage;
                handler.knockbackForce = knockbackForce;
                handler.isFreezing = isFreezing;
                handler.freezeDuration = freezeDuration;
            }
        }
        // Destroy this grenade game object
        Destroy(gameObject);
    }

}