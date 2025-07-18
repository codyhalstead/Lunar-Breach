using UnityEngine;
using UnityEngine.Audio;

public class PrimaryGun4 : WeaponBase
{
    public Transform firePoint;
    [SerializeField] public float fireForce = 10f;
    [SerializeField] public int bulletDamage = 10;
    [SerializeField] public float knockBackForce = 10f;
    [SerializeField] public float fireRate = 0.2f;
    [SerializeField] private AudioSource fireAudioSource;
    public AudioClip firingSound;

    private float maxFireDuration = 4.7f; 
    private float fireHeldTimer = 0f;

    private float cooldown = 0f;
    private bool isFiringHeld = false;
    private bool isOverheated = false;


    void Update()
    {
        if (isFiringHeld && !isOverheated)
        {
            fireHeldTimer += Time.deltaTime;
            if (cooldown <= 0f)
            {
                Fire();
                cooldown = fireRate;
            }
            if (fireHeldTimer >= maxFireDuration)
            {
                isOverheated = true;
                fireHeldTimer = 0f;
            }
        }
        else
        {
            fireHeldTimer = 0f;
        }
        cooldown -= Time.deltaTime;
        if (isFiringHeld && !fireAudioSource.isPlaying && !isOverheated)
        {
            fireAudioSource.clip = firingSound;
            fireAudioSource.Play();
        }
        else if ((!isFiringHeld || isOverheated) && fireAudioSource.isPlaying)
        {
            fireAudioSource.Stop();
        }
    }

    public override void HoldFire(bool isHeld)
    {
        if (!isHeld)
        {
            isOverheated = false;
            fireHeldTimer = 0f;
        }
        isFiringHeld = isHeld;
    }

    public override void Fire()
    {
        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply force to bullet
            rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
        // Apply the collision script
        BulletCollision bulletScript = bullet.GetComponent<BulletCollision>();
        if (bulletScript != null)
        {
            // Pass on weapon information to script
            bulletScript.damage = bulletDamage;
            bulletScript.knockbackForce = knockBackForce;
        }
    }

    public override void SetFirePoint(Transform point)
    {
        firePoint = point;
    }

}
