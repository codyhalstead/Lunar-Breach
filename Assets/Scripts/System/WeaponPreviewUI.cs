using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPreviewUI : MonoBehaviour
{
    public GameObject weaponPreviewWindow;
    public TextMeshProUGUI nameText;
    public Image bulletPreviewImage;
    public GameObject purchaseUI;
    public TextMeshProUGUI costText;
    public Button purchaseButton;
    public Button equipButton;

    private WeaponBase selectedWeapon;
    private WeaponShopUI shop;

    public void Setup(WeaponShopUI shop)
    {
        this.shop = shop;
    }


    public void hide()
    {
        weaponPreviewWindow.SetActive(false);
    }

    public void show(WeaponBase weapon, bool isOwned, bool isEquipped)
    {
        this.selectedWeapon = weapon;
        weaponPreviewWindow.SetActive(true);
        setBulletPreview(weapon);
        nameText.text = weapon.weaponName;
        if (isOwned)
        {
            purchaseUI.SetActive(false);
            costText.text = "0";
            if (isEquipped)
            {
                equipButton.gameObject.SetActive(false);
            }
            else
            {
                equipButton.gameObject.SetActive(true);
            }
        }
        else
        {
            purchaseUI.SetActive(true);
            costText.text = weapon.weaponCost.ToString();
            equipButton.gameObject.SetActive(false);
        }
    }

    private Sprite getBulletSprite(WeaponBase weapon)
    {
        if (weapon != null && weapon.bulletPrefab != null)
        {
            GameObject bullet = weapon.bulletPrefab;
            Sprite bulletSprite = bullet.GetComponent<SpriteRenderer>()?.sprite;
            return bulletSprite;
        }
        return null;
    }

    public void setBulletPreview(WeaponBase weapon)
    {
        Sprite bulletSprite = getBulletSprite(weapon);
        if (bulletSprite != null)
        {
            bulletPreviewImage.gameObject.SetActive(true);
            bulletPreviewImage.sprite = bulletSprite;
        }
        else
        {
            bulletPreviewImage.gameObject.SetActive(false);
        }
    }

    public void purchaseClicked()
    {
        shop.purchaseWeapon(selectedWeapon);
    }

    public void equippedClicked()
    {
        shop.equipWeapon(selectedWeapon);
    }

}
