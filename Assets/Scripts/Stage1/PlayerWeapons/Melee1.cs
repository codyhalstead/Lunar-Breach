using UnityEngine;

public class Melee1 : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform playerAimer;
    [SerializeField] public float fireForce = 10f;
    [SerializeField] public int bulletDamage = 10;
    [SerializeField] public float knockBackForce = 10f;

    public override void Fire()
    {
        // Instantiate the melee hitbox as a child of PlayerAimer
        //firePoint.rotation = playerAimer.rotation;
        //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, playerAimer.rotation);
        GameObject hitbox = Instantiate(bulletPrefab, firePoint);
        hitbox.transform.localPosition = Vector3.zero;  // stick to firePoint's center
        hitbox.transform.localRotation = playerAimer.rotation; // align with aiming direction

        BulletCollision bulletScript = hitbox.GetComponent<BulletCollision>();
        if (bulletScript != null)
        {
            bulletScript.destroyOnImpact = false;
            Debug.LogWarning("BULLETSCRIPT is real!");
            bulletScript.damage = bulletDamage;
            bulletScript.knockbackForce = knockBackForce;
        }

        Destroy(hitbox, 0.3f); // destroy after short duration
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
