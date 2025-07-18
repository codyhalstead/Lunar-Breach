using System.Collections;
using UnityEngine;

public class Melee3 : WeaponBase
{
    private bool isSwinging = false;
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
        if (isSwinging)
        {
            // Previous swing in progress, ignore
            return;
        }
        if (swingSound != null && fireAudioSource != null)
        {
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
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 180f);
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
            // Limit swing lifetime
            StartCoroutine(SwingDuration(0.3f));
            // Set as "swinging"
            isSwinging = true;
        }
    }

    private IEnumerator SwingDuration(float time)
    {
        // Completes swing after given duration
        yield return new WaitForSeconds(time);
        CompleteSwing();
    }

    public void CompleteSwing()
    {
        // Destroy "swing" object, set as not "swinging"
        Destroy(hitbox);
        isSwinging = false;
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