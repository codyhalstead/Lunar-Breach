using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConsumableManager : MonoBehaviour
{
    private GameDataManager gameDataManager;
    [SerializeField] public int healValue = 40;
    [SerializeField] private ConsumableUI consumableUI;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerHealth playerHealth;

    void Start()
    {
        gameDataManager = GameDataManager.GetInstance();
        // Update UI to show consumable amount
        if (consumableUI != null)
        {
            consumableUI.UpdateConsumableCount(gameDataManager.CurrentData.medkits);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            // Do not use if time is froze (ex: paused)
            return;
        }

        // If consumable button pressed and player has medkits
        // Heal player damage, decrement available medkit count, update UI
        if (playerInput.actions["Crouch"].triggered && playerHealth != null && gameDataManager.CurrentData.medkits > 0)
        {
            playerHealth.HealDamage(healValue);
            gameDataManager.CurrentData.medkits--;
            consumableUI.UpdateConsumableCount(gameDataManager.CurrentData.medkits);
        }

    }
}
