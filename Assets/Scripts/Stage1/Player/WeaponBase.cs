using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;
    //public GameObject weapon;

    public abstract void Fire();

    public virtual void SetFirePoint(Transform point)
    {
       
    }

    public virtual void SetFireAim(Transform point)
    {

    }
}
