using TMPro;
using UnityEngine;

public class PlayerSecAmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI grenadeCountText;

    public void UpdateGrenadeCount(int count)
    {
        // Update secondary ammo UI
        grenadeCountText.text = "x" + count;
    }
}
