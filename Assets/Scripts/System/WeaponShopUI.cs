using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponShopUI : MonoBehaviour
{
    public int medkitPrice;

    public GameObject weaponRowPrefab;
    public GameObject medMenu;
    public GameObject PurchaseWeaponMenu;

    public Transform contentPanel;
    public GameObject scrollView;
    public List<WeaponBase> primaryWeapons;
    public List<WeaponBase> secondaryWeapons;
    public List<WeaponBase> meleeWeapons;

    private WeaponPreviewUI weaponPreviewUI;

    private GameDataManager gameDataManager;

    private PlayerCurrency playerCurrency;
    private PlayerMedkits playerMedkits;
    private WeaponType currentWeaponType;

    public enum WeaponType
    {
        Primary = 1,
        Secondary = 2,
        Melee = 3
    }

    void Start()
    {
        gameDataManager = GameDataManager.GetInstance();
        playerMedkits = GetComponent<PlayerMedkits>();
        playerCurrency = GetComponent<PlayerCurrency>();
        weaponPreviewUI = GetComponent<WeaponPreviewUI>();
        weaponPreviewUI.Setup(this, gameDataManager.CurrentData.languageCode);
        MedkitShopUI medUI = medMenu.GetComponent<MedkitShopUI>();
        if (medUI != null)
        {
            medUI.Setup(medkitPrice);
        }
        loadPrimaries();
        loadSecondaries();
        loadMelee();
        PopulateShop(primaryWeapons, WeaponType.Primary);
    }

    public void PopulateShop(List<WeaponBase> weaponList, WeaponType weaponType)
    {
        scrollView.SetActive(true);
        weaponPreviewUI.hide();
        medMenu.SetActive(false);
        currentWeaponType = weaponType;
        ClearShop();
        string equippedtWeaponName = getCurrentEquippedWeaponName();
        foreach (var weapon in weaponList)
        {
            GameObject row = Instantiate(weaponRowPrefab, contentPanel);
            WeaponRowUI rowUI = row.GetComponent<WeaponRowUI>();
            rowUI.Setup(weapon, this, gameDataManager.CurrentData.languageCode);
            if (weapon.name == equippedtWeaponName)
            {
                rowUI.setEquipped();
            }
            if (isWeaponOwned(weapon.name))
            {
                rowUI.setPurchased();
            }
        }
    }

    public void ClearShop()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowPrimaryWeapons()
    {
        PopulateShop(primaryWeapons, WeaponType.Primary);
    }

    public void ShowSecondaryWeapons()
    {
        PopulateShop(secondaryWeapons, WeaponType.Secondary);
    }

    public void ShowMeleeWeapons()
    {
        PopulateShop(meleeWeapons, WeaponType.Melee);
    }

    public void ShowMedkits()
    {
        scrollView.SetActive(false);
        weaponPreviewUI.hide();
        medMenu.SetActive(true);
    }

    private void loadPrimaries()
    {
        primaryWeapons.Clear();
        foreach (string name in gameDataManager.allPrimaries)
        {
            WeaponBase prefab = Resources.Load<WeaponBase>($"Prefabs/Primaries/{name}");
            if (prefab != null)
            {
                primaryWeapons.Add(prefab);
            }
            else
            {
                Debug.LogWarning($"Weapon prefab '{name}' not found in Prefabs/Primaries/");
            }
        }
    }

    private void loadSecondaries()
    {
        secondaryWeapons.Clear();
        foreach (string name in gameDataManager.allSecondaries)
        {
            WeaponBase prefab = Resources.Load<WeaponBase>($"Prefabs/Secondaries/{name}");
            if (prefab != null)
            {
                secondaryWeapons.Add(prefab);
            }
            else
            {
                Debug.LogWarning($"Weapon prefab '{name}' not found in Prefabs/Secondaries/");
            }
        }
    }

    private void loadMelee()
    {
        meleeWeapons.Clear();
        foreach (string name in gameDataManager.allMelee)
        {
            WeaponBase prefab = Resources.Load<WeaponBase>($"Prefabs/Melee/{name}");
            if (prefab != null)
            {
                meleeWeapons.Add(prefab);
            }
            else
            {
                Debug.LogWarning($"Weapon prefab '{name}' not found in Prefabs/Melee/");
            }
        }
    }

    public void onBackPressed()
    {
        gameDataManager.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    public void purchaseMedkits(int amount)
    {
        int totalPrice = medkitPrice * amount;
        if (playerCurrency.RemoveCurrency(totalPrice))
        {
            playerMedkits.AddMedkits(amount);
        }
    }

    public void purchaseWeapon(WeaponBase weapon)
    {
        if (playerCurrency.RemoveCurrency(weapon.weaponCost))
        {
            unlockWeapon(weapon.name);
            refreshUI();
        }
    }

    private void equipPrimaryWeapon(WeaponBase weapon)
    {
        gameDataManager.CurrentData.equippedPrimary = weapon.name;
    }

    private void equipSecondaryWeapon(WeaponBase weapon)
    {
        gameDataManager.CurrentData.equippedSecondary = weapon.name;
    }

    private void equipMeleeWeapon(WeaponBase weapon)
    {
        gameDataManager.CurrentData.equippedMelee = weapon.name;
    }

    public void equipWeapon(WeaponBase weapon)
    {
        if (currentWeaponType == WeaponType.Primary)
        {
            equipPrimaryWeapon(weapon);
        }
        else if (currentWeaponType == WeaponType.Secondary)
        {
            equipSecondaryWeapon(weapon);
        }
        else if (currentWeaponType == WeaponType.Melee)
        {
            equipMeleeWeapon(weapon);
        }
        refreshUI();
    }

    public void OnWeaponSelected(WeaponBase selectedWeapon, bool isOwned, bool isEquipped)
    {
        weaponPreviewUI.show(selectedWeapon, isOwned, isEquipped);
    }

    private string getCurrentEquippedWeaponName()
    {
        string currentEquippedWeapon = "";
        if (currentWeaponType == WeaponType.Primary)
        {
            currentEquippedWeapon = gameDataManager.CurrentData.equippedPrimary;
        }
        else if (currentWeaponType == WeaponType.Secondary)
        {
            currentEquippedWeapon = gameDataManager.CurrentData.equippedSecondary;
        }
        else if (currentWeaponType == WeaponType.Melee)
        {
            currentEquippedWeapon = gameDataManager.CurrentData.equippedMelee;
        }
        return currentEquippedWeapon;
    }

    private bool isWeaponOwned(string weaponName)
    {
        if (currentWeaponType == WeaponType.Primary)
        {
            if (gameDataManager.CurrentData.unlockedPrimaries.Contains(weaponName))
            {
                return true;
            }
        }
        else if (currentWeaponType == WeaponType.Secondary)
        {
            if (gameDataManager.CurrentData.unlockedSecondaries.Contains(weaponName))
            {
                return true;
            }
        }
        else if (currentWeaponType == WeaponType.Melee)
        {
            if (gameDataManager.CurrentData.unlockedMelee.Contains(weaponName))
            {
                return true;
            }
        }
        return false;
    }

    private void unlockWeapon(string weaponName)
    {
        if (currentWeaponType == WeaponType.Primary)
        {
            gameDataManager.CurrentData.unlockedPrimaries.Add(weaponName);
        }
        else if (currentWeaponType == WeaponType.Secondary)
        {
            gameDataManager.CurrentData.unlockedSecondaries.Add(weaponName);
        }
        else if (currentWeaponType == WeaponType.Melee)
        {
            gameDataManager.CurrentData.unlockedMelee.Add(weaponName);
        }
    }

    private void refreshUI()
    {
        if (currentWeaponType == WeaponType.Primary)
        {
            PopulateShop(primaryWeapons, WeaponType.Primary);
        }
        else if (currentWeaponType == WeaponType.Secondary)
        {
            PopulateShop(secondaryWeapons, WeaponType.Secondary);
        }
        else if (currentWeaponType == WeaponType.Melee)
        {
            PopulateShop(meleeWeapons, WeaponType.Melee);
        }
    }

}
