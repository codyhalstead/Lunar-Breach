using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    
    public Transform weaponHolder;
    private PlayerInput playerInput;
    Transform playerAimer;
    Transform player;

    // Primary
    public List<GameObject> unlockedPrimaryPrefabs = new List<GameObject>();
    private int currentPrimaryIndex = 0;
    public GameObject defaultPrimaryPrefab;
    private GameObject currentPrimaryObject;
    private WeaponBase currentPrimaryScript;

    // Secondary
    public List<GameObject> unlockedSecondaryPrefabs = new List<GameObject>();
    private int currentSecondaryIndex = 0;
    public GameObject defaultSecondaryPrefab;
    private GameObject currentSecondaryObject;
    private WeaponBase currentSecondaryScript;
    [SerializeField] private PlayerSecAmmoUI ammoUI;

    // Melee
    public List<GameObject> unlockedMeleePrefabs = new List<GameObject>();
    private int currentMeleeIndex = 0;
    public GameObject defaultMeleePrefab;
    private GameObject currentMeleeObject;
    private WeaponBase currentMeleeScript;

    void Awake()
    {
        // Automatically reference Player, PlayerInput, and orbiting Aimer 
        playerInput = GetComponentInParent<PlayerInput>();
        playerAimer = GameObject.Find("Player/PlayerAimer")?.transform;
        player = GameObject.Find("Player")?.transform;
    }

    void Start()
    {
        // Equip selected primary (or default)
        if (unlockedPrimaryPrefabs.Count > 0)
        {
            EquipPrimaryWeapon(0);
        }
        else if (defaultPrimaryPrefab != null)
        {
            EquipDefaultPrimaryWeapon();
        }
        // Equip selected secondary (or default)
        if (unlockedSecondaryPrefabs.Count > 0)
        {
            EquipSecondaryWeapon(0);
        }
        else if (defaultSecondaryPrefab != null)
        {
            EquipDefaultSecondaryWeapon();
        }
        // Equip selected melee (or default)
        if (unlockedMeleePrefabs.Count > 0)
        {
            EquipMeleeWeapon(0);
        }
        else if (defaultMeleePrefab != null)
        {
            EquipDefaultMeleeWeapon();
        }
        // Set ammo UI to display secondary ammo count
        if (ammoUI != null)
        {
            ammoUI.UpdateGrenadeCount(currentSecondaryScript.GetCurrentAmmo());
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            // Do not spawn attacks if time is froze (ex: paused)
            return;
        }

        // Check if fire button is held. Pass information to primary weapon for firing
        float attackHeld = playerInput.actions["Attack"].ReadValue<float>();
        currentPrimaryScript?.HoldFire(attackHeld > 0f);
        // Secondary attack button pressed, fire secondary
        if (playerInput.actions["SecAttack"].triggered && currentSecondaryScript != null)
        {
            currentSecondaryScript.Fire();
            if (ammoUI != null)
            {
                // Update secondary ammo UI
                ammoUI.UpdateGrenadeCount(currentSecondaryScript.GetCurrentAmmo());
            }
        }
        // Secondary attack button pressed, fire melee
        if (playerInput.actions["MeleeAttack"].triggered && currentMeleeScript != null)
        {
            currentMeleeScript.Fire();
        }
    }

    void EquipPrimaryWeapon(int index)
    {
        // Instantiate and equip selected primary
        // TODO: Only a draft, WIP
        if (index < 0 || index >= unlockedPrimaryPrefabs.Count)
        {
            return;
        }
        if (currentPrimaryObject != null)
        {
            Destroy(currentPrimaryObject);
        }
        currentPrimaryObject = Instantiate(unlockedPrimaryPrefabs[index], weaponHolder);
        currentPrimaryScript = currentPrimaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentPrimaryScript, playerAimer);
        currentPrimaryIndex = index;
    }

    void EquipDefaultPrimaryWeapon()
    {
        // Instantiate and equip default primary
        currentPrimaryObject = Instantiate(defaultPrimaryPrefab, weaponHolder);
        currentPrimaryScript = currentPrimaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentPrimaryScript, playerAimer);
        currentPrimaryIndex = 0;
    }

    void EquipSecondaryWeapon(int index)
    {
        // Instantiate and equip selected secondary
        // TODO: Only a draft, WIP
        if (index < 0 || index >= unlockedSecondaryPrefabs.Count)
        {
            return;
        }
        if (currentSecondaryObject != null)
        {
            Destroy(currentSecondaryObject);
        }
        currentSecondaryObject = Instantiate(unlockedSecondaryPrefabs[index], weaponHolder);
        currentSecondaryScript = currentSecondaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentSecondaryScript, playerAimer);
        currentSecondaryIndex = index;
    }

    void EquipDefaultSecondaryWeapon()
    {
        // Instantiate and equip default secondary
        currentSecondaryObject = Instantiate(defaultSecondaryPrefab, weaponHolder);
        currentSecondaryScript = currentSecondaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentSecondaryScript, playerAimer);
        currentSecondaryIndex = 0;
    }

    void EquipMeleeWeapon(int index)
    {
        // Instantiate and equip selected melee
        // TODO: Only a draft, WIP
        if (index < 0 || index >= unlockedMeleePrefabs.Count)
        {
            return;
        }
        if (currentMeleeObject != null)
        {
            Destroy(currentMeleeObject);
        }
        currentMeleeObject = Instantiate(unlockedMeleePrefabs[index], weaponHolder);
        currentMeleeScript = currentMeleeObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentMeleeScript, player);
        AssignFireAim(currentMeleeScript, playerAimer);
        currentMeleeIndex = index;
    }

    void EquipDefaultMeleeWeapon()
    {
        // Instantiate and equip default melee
        currentMeleeObject = Instantiate(defaultMeleePrefab, weaponHolder);
        currentMeleeScript = currentMeleeObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentMeleeScript, player);
        AssignFireAim(currentMeleeScript, playerAimer);
        currentMeleeIndex = 0;
    }

    public void UnlockWeapon(GameObject weaponPrefab)
    {
        // TODO
        //
        //if (!unlockedPrimaryPrefabs.Contains(weaponPrefab))
        //{
        //    unlockedPrimaryPrefabs.Add(weaponPrefab);
        //    if (unlockedPrimaryPrefabs.Count == 1)
        //    {
                // EquipPrimaryWeapon(0);
        //    }
        //}
    }

    private void AssignFirePoint(WeaponBase weaponScript, Transform transform)
    {
        // Assign firepoint to a weapon
        if (weaponScript == null)
        {
            return;
        }
        if (playerAimer != null)
        {
            weaponScript.SetFirePoint(transform); 
        }
        else
        {
            Debug.LogWarning("Firepoint not found");
        }
    }

    private void AssignFireAim(WeaponBase weaponScript, Transform transform)
    {
        // Assign fireaim to a weapon
        if (weaponScript == null)
        {
            return;
        }
        if (playerAimer != null)
        {
            weaponScript.SetFireAim(transform);
        }
        else
        {
            Debug.LogWarning("FireAimer not found");
        }
    }
}