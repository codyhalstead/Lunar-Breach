using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponRowUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public GameObject equippedIcon;
    public GameObject priceUI;
    public TextMeshProUGUI costText;
    public bool isOwned;
    public bool isEquipped;

    private WeaponBase weapon;
    private WeaponShopUI shop;
    private string languageCode = "en";

    public void Setup(WeaponBase weapon, WeaponShopUI shop, string languageCode)
    {
        this.weapon = weapon;
        this.shop = shop;
        this.languageCode = languageCode;
        if (languageCode == "es")
        {
            nameText.text = weapon.spanishWeaponName;

        }
        else
        {
            nameText.text = weapon.weaponName;
        }
        if (isOwned)
        {
            priceUI.SetActive(false);
            if (isEquipped)
            {
                equippedIcon.SetActive(true);
            }
            else
            {
                equippedIcon.SetActive(false);
            }
        } 
        else
        {
            priceUI.SetActive(true);
            costText.text = weapon.weaponCost + "";
            equippedIcon.SetActive(false);
        }
    }

    public void setPurchased()
    {
        priceUI.SetActive(false);
        isOwned = true;
    }

    public void setSold()
    {
        priceUI.SetActive(true);
        equippedIcon.SetActive(false);
        isOwned = false;
        isEquipped = false;
        costText.text = weapon.weaponCost + "";

    }

    public void setEquipped()
    {
        equippedIcon.SetActive(true);
        isEquipped = true;
    }

    public void setUnEquipped()
    {
        equippedIcon.SetActive(false);
        isEquipped = false;
    }

    public WeaponBase GetWeapon()
    {
        return weapon;
    }

    public void OnRowClicked()
    {
        if (shop != null)
        {
            shop.OnWeaponSelected(weapon, isOwned, isEquipped);
        }
    }

}