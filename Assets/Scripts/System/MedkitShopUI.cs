using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MedkitShopUI : MonoBehaviour
{
    public TextMeshProUGUI oneMedkitPrice;
    public TextMeshProUGUI fiveMedkitPrice;
    public TextMeshProUGUI tenMedkitPrice;

    public void Setup(int price)
    {
        oneMedkitPrice.text = price.ToString();
        fiveMedkitPrice.text = (price * 5).ToString();
        tenMedkitPrice.text = (price * 10).ToString();
    }
}