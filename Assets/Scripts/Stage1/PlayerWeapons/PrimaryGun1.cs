using UnityEngine;

public class PrimaryGun1 : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] public float fireForce = 10f;
    [SerializeField] public int bulletDamage = 10;
    [SerializeField] public float knockBackForce = 10f;

    public override void Fire()
    {
        //Debug.LogError("GUN TRYING TO FIRE");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //rb.AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
            rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
        BulletCollision bulletScript = bullet.GetComponent<BulletCollision>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage;
            bulletScript.knockbackForce = knockBackForce;
        }
    }

    public override void SetFirePoint(Transform point)
    {
        firePoint = point;
    }

}
