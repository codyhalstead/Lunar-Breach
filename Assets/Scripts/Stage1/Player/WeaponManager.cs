using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    
    public Transform weaponHolder;
    private PlayerInput playerInput;
    Transform playerAimer;
    Transform player;

    //Primary
    public List<GameObject> unlockedPrimaryPrefabs = new List<GameObject>();
    private int currentPrimaryIndex = 0;
    public GameObject defaultPrimaryPrefab;
    private GameObject currentPrimaryObject;
    private WeaponBase currentPrimaryScript;

    //Secondary
    public List<GameObject> unlockedSecondaryPrefabs = new List<GameObject>();
    private int currentSecondaryIndex = 0;
    public GameObject defaultSecondaryPrefab;
    private GameObject currentSecondaryObject;
    private WeaponBase currentSecondaryScript;

    //Melee
    public List<GameObject> unlockedMeleePrefabs = new List<GameObject>();
    private int currentMeleeIndex = 0;
    public GameObject defaultMeleePrefab;
    private GameObject currentMeleeObject;
    private WeaponBase currentMeleeScript;






    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        playerAimer = GameObject.Find("Player/PlayerAimer")?.transform;
        player = GameObject.Find("Player")?.transform;
    }

    void Start()
    {
        if (unlockedPrimaryPrefabs.Count > 0)
            EquipPrimaryWeapon(0);
        else if (defaultPrimaryPrefab != null)
            EquipDefaultPrimaryWeapon();

        if (unlockedSecondaryPrefabs.Count > 0)
            EquipSecondaryWeapon(0);
        else if (defaultSecondaryPrefab != null)
            EquipDefaultSecondaryWeapon();

        if (unlockedMeleePrefabs.Count > 0)
            EquipMeleeWeapon(0);
        else if (defaultMeleePrefab != null)
            EquipDefaultMeleeWeapon();
    }

    void Update()
    {
        if (playerInput.actions["Attack"].triggered && currentPrimaryScript != null)
            currentPrimaryScript.Fire();

        if (playerInput.actions["SecAttack"].triggered && currentSecondaryScript != null)
            currentSecondaryScript.Fire();

        if (playerInput.actions["MeleeAttack"].triggered && currentMeleeScript != null)
            currentMeleeScript.Fire();

        //float scroll = Mouse.current.scroll.ReadValue().y;
        //if (scroll != 0)
        //{
        //    int newIndex = Mathf.Clamp(currentIndex + (scroll > 0 ? 1 : -1), 0, unlockedWeaponPrefabs.Count - 1);
        //    if (newIndex != currentIndex)
        //        EquipWeapon(newIndex);
        //}
    }

    void EquipPrimaryWeapon(int index)
    {
        if (index < 0 || index >= unlockedPrimaryPrefabs.Count) return;

        if (currentPrimaryObject != null)
            Destroy(currentPrimaryObject);

        currentPrimaryObject = Instantiate(unlockedPrimaryPrefabs[index], weaponHolder);
        currentPrimaryScript = currentPrimaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentPrimaryScript, playerAimer);
        currentPrimaryIndex = index;
    }

    void EquipDefaultPrimaryWeapon()
    {
        currentPrimaryObject = Instantiate(defaultPrimaryPrefab, weaponHolder);
        currentPrimaryScript = currentPrimaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentPrimaryScript, playerAimer);

        currentPrimaryIndex = 0;
    }

    void EquipSecondaryWeapon(int index)
    {
        if (index < 0 || index >= unlockedSecondaryPrefabs.Count) return;

        if (currentSecondaryObject != null)
            Destroy(currentSecondaryObject);

        currentSecondaryObject = Instantiate(unlockedSecondaryPrefabs[index], weaponHolder);
        currentSecondaryScript = currentSecondaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentSecondaryScript, playerAimer);
        currentSecondaryIndex = index;
    }

    void EquipDefaultSecondaryWeapon()
    {
        currentSecondaryObject = Instantiate(defaultSecondaryPrefab, weaponHolder);
        currentSecondaryScript = currentSecondaryObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentSecondaryScript, playerAimer);

        currentSecondaryIndex = 0;
    }

    void EquipMeleeWeapon(int index)
    {
        if (index < 0 || index >= unlockedMeleePrefabs.Count) return;

        if (currentMeleeObject != null)
            Destroy(currentMeleeObject);

        currentMeleeObject = Instantiate(unlockedMeleePrefabs[index], weaponHolder);
        currentMeleeScript = currentMeleeObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentMeleeScript, player);
        AssignFireAim(currentMeleeScript, playerAimer);
        currentMeleeIndex = index;
    }

    void EquipDefaultMeleeWeapon()
    {
        currentMeleeObject = Instantiate(defaultMeleePrefab, weaponHolder);
        currentMeleeScript = currentMeleeObject.GetComponent<WeaponBase>();
        AssignFirePoint(currentMeleeScript, player);
        AssignFireAim(currentMeleeScript, playerAimer);
        currentMeleeIndex = 0;
    }

    public void UnlockWeapon(GameObject weaponPrefab)
    {
        if (!unlockedPrimaryPrefabs.Contains(weaponPrefab))
        {
            unlockedPrimaryPrefabs.Add(weaponPrefab);
            if (unlockedPrimaryPrefabs.Count == 1)
            {
                // EquipPrimaryWeapon(0);
            }
        }
    }

    private void AssignFirePoint(WeaponBase weaponScript, Transform transform)
    {
        if (weaponScript == null) return;
        if (playerAimer != null)
        {
            weaponScript.SetFirePoint(transform); 
        }
        else
        {
            Debug.LogWarning("Firepoint not found in scene!");
        }
    }

    private void AssignFireAim(WeaponBase weaponScript, Transform transform)
    {
        if (weaponScript == null) return;
        if (playerAimer != null)
        {
            weaponScript.SetFireAim(transform);
        }
        else
        {
            Debug.LogWarning("FireAimer not found in scene!");
        }
    }
}