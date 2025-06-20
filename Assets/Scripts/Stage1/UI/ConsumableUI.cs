using TMPro;
using UnityEngine;

public class ConsumableUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI consumableCountText;

    public void UpdateConsumableCount(int count)
    {
        // Update consumable UI
        consumableCountText.text = "x" + count;
    }
}
