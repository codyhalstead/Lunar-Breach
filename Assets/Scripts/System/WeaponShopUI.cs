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

    void Start()
    {
        gameDataManager = GameDataManager.GetInstance();
        playerMedkits = GetComponent<PlayerMedkits>();
        playerCurrency = GetComponent<PlayerCurrency>();
        weaponPreviewUI = GetComponent<WeaponPreviewUI>();
        weaponPreviewUI.Setup(this);
        MedkitShopUI medUI = medMenu.GetComponent<MedkitShopUI>();
        if (medUI != null)
        {
            medUI.Setup(medkitPrice);
        }
        loadPrimaries();
        loadSecondaries();
        loadMelee();
        PopulateShop(primaryWeapons);
    }

    public void PopulateShop(List<WeaponBase> weaponList)
    {
        scrollView.SetActive(true);
        weaponPreviewUI.hide();
        medMenu.SetActive(false);
        ClearShop();
        foreach (var weapon in weaponList)
        {
            GameObject row = Instantiate(weaponRowPrefab, contentPanel);
            WeaponRowUI rowUI = row.GetComponent<WeaponRowUI>();
            rowUI.Setup(weapon, this);
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
        PopulateShop(primaryWeapons);
    }

    public void ShowSecondaryWeapons()
    {
        PopulateShop(secondaryWeapons);
    }

    public void ShowMeleeWeapons()
    {
        PopulateShop(meleeWeapons);
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

    }

    public void equipWeapon(WeaponBase weapon)
    {
        
    }


    public void OnWeaponSelected(WeaponBase selectedWeapon, bool isOwned, bool isEquipped)
    {
        weaponPreviewUI.show(selectedWeapon, isOwned);
    }

}
