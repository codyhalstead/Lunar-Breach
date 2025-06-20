using UnityEngine;

public class PlayerMedkits : MonoBehaviour
{
    public ConsumableUI consumableUI;
    private GameDataManager gameDataManager;

    void Start()
    {
        gameDataManager = GameDataManager.GetInstance();
        consumableUI.UpdateConsumableCount(GameDataManager.GetInstance().CurrentData.medkits);
    }

    public void AddMedkits(int amount)
    {
        gameDataManager.CurrentData.medkits += amount;
        if (gameDataManager.CurrentData.medkits > 999)
        {
            gameDataManager.CurrentData.medkits = 999;
        }
        consumableUI.UpdateConsumableCount(gameDataManager.CurrentData.medkits);
    }

    public bool RemoveMedkits(int amount)
    {
        if (amount <= gameDataManager.CurrentData.medkits)
        {
            gameDataManager.CurrentData.currency -= amount;
            consumableUI.UpdateConsumableCount(gameDataManager.CurrentData.medkits);
            return true;
        }
        return false;
    }

}