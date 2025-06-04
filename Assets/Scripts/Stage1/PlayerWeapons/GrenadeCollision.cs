using System;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCollision : MonoBehaviour
{
    public float explosionDelay = 1f;
    public float explosionRadius = 2f;
    public int damage = 50;
    public float knockbackForce = 10f;
    public GameObject explosionEffect;
    

    private bool hasExploded = false;

    void Start()
    {
        Invoke(nameof(Explode), explosionDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            ExplosionEventHandler handler = explosion.GetComponent<ExplosionEventHandler>();
            if (handler != null)
            {
                handler.sourceGrenade = gameObject;
                handler.damage = damage;
                handler.knockbackForce = knockbackForce;
            }
        }
        Destroy(gameObject);
    }

}