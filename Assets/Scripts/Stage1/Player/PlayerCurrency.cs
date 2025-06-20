using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public PlayerCurrencyUI currencyUI;
    private GameDataManager gameDataManager;

    void Start()
    {
        gameDataManager = GameDataManager.GetInstance();
        currencyUI.UpdateCurrencyCount(GameDataManager.GetInstance().CurrentData.currency);
    }

    public void AddCurrency(int amount)
    {
        gameDataManager.CurrentData.currency += amount;
        if (gameDataManager.CurrentData.currency > 9999999)
        {
            gameDataManager.CurrentData.currency = 9999999;
        }
        currencyUI.UpdateCurrencyCount(gameDataManager.CurrentData.currency);
    }

    public bool RemoveCurrency(int amount)
    {
        if (amount <= gameDataManager.CurrentData.currency)
        {
            gameDataManager.CurrentData.currency -= amount;
            currencyUI.UpdateCurrencyCount(gameDataManager.CurrentData.currency);
            return true;
        }
        currencyUI.FlashRedTwice();
        return false;
    }

}
