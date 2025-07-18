using System.Collections;
using UnityEngine;

public class Melee2 : WeaponBase
{
    public Transform firePoint;
    public Transform playerAimer;
    private GameObject hitbox;
    [SerializeField] public float fireForce = 10f;
    [SerializeField] public int bulletDamage = 10;
    [SerializeField] public float knockBackForce = 10f;
    public float forwardOffset = 2f;
    public float verticalOffset = 0f;
    [SerializeField] private AudioSource fireAudioSource;
    public AudioClip swingSound;

    public override void Fire()
    {
        if (swingSound != null && fireAudioSource != null)
        {
            fireAudioSource.pitch = Random.Range(0.70f, 1.3f);
            fireAudioSource.PlayOneShot(swingSound);
        }
        // Adjust targeting and firepoint positions with offsets
        // Used to accomodate sprite (both enemy and attack) misallignments
        Vector2 targetOffset = new Vector2(0f, verticalOffset);
        Vector2 adjustedTargetPos = (Vector2)playerAimer.position + targetOffset;
        // Calc attack direction
        Vector2 shootDir = (adjustedTargetPos - (Vector2)firePoint.position).normalized;
        // Rotate bullet (attack) sprite to match firing angle
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        // Calculate bullet (attack) spawn position
        Vector2 spawnPosition = (Vector2)firePoint.position
                              + shootDir * forwardOffset
                              + Vector2.down * verticalOffset;
        // Create bullet (attack)
        hitbox = Instantiate(bulletPrefab, spawnPosition, rotation);

        // Apply the collision script
        BulletCollision bulletScript = hitbox.GetComponent<BulletCollision>();
        if (bulletScript != null)
        {
            // Pass on weapon information to script
            bulletScript.destroyOnImpact = false;
            bulletScript.noMultiHits = true;
            bulletScript.damage = bulletDamage;
            bulletScript.knockbackForce = knockBackForce;
        }
    }

    public override void SetFirePoint(Transform point)
    {
        firePoint = point;
    }

    public override void SetFireAim(Transform point)
    {
        playerAimer = point;
    }
}
