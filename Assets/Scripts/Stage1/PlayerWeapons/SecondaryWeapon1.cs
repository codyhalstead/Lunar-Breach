using UnityEngine;

public class SecondaryWeapon1 : WeaponBase
{
    public GameObject grenadePrefab;
    public Transform firePoint;
    [SerializeField] public float fireForce = 10f;
    [SerializeField] public int grenadeDamage = 50;
    [SerializeField] public float knockBackForce = 10f;
    [SerializeField] public float explosionDelay = 3f;
    [SerializeField] public float explosionRadius = 2f;

    public override void Fire()
    {
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            rb.angularVelocity = 100f;
        }
        GrenadeCollision grenadeScript = grenade.GetComponent<GrenadeCollision>();
        if (grenadeScript != null)
        {
            grenadeScript.damage = grenadeDamage;
            grenadeScript.knockbackForce = knockBackForce;
            grenadeScript.explosionDelay = explosionDelay;
            grenadeScript.explosionRadius = explosionRadius;
        }
    }

    public override void SetFirePoint(Transform point)
    {
        firePoint = point;
    }

}