using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Button level2Button;
    public UnityEngine.UI.Button level3Button;
    public AchievementsUI achievementsUI;
    private GameDataManager gameDataManager;

    void Start()
    {
        Cursor.visible = true;
        StartCoroutine(InitializeMenu());
    }

    private IEnumerator InitializeMenu()
    {
        // Wait for localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Load data and set the correct locale
        gameDataManager = GameDataManager.GetInstance();

        String localization = gameDataManager.CurrentData.languageCode;

        var locale = LocalizationSettings.AvailableLocales.Locales
            .Find(l => l.Identifier.Code == localization);

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }

        // Now enable/disable buttons based on game progress
        if (!gameDataManager.CurrentData.completedStages.Contains("Stage1"))
        {
            level2Button.interactable = false;
        }
        if (!gameDataManager.CurrentData.completedStages.Contains("Stage2"))
        {
            level3Button.interactable = false;
        }
    }

    public void LaunchLevel1() {
        SceneManager.LoadScene("Stage1");
    }

    public void LaunchLevel2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void LaunchLevel3()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("ItemShop");
    }

    public void LoadAchievements()
    {
        if (achievementsUI != null )
        {
            achievementsUI.SetUp(gameDataManager.CurrentData);
        }
    }

    public void SwitchToEnglish()
    {
        StartCoroutine(SetLocale("en"));
        gameDataManager.CurrentData.languageCode = "en";
        gameDataManager.SaveData();
    }

    public void SwitchToSpanish()
    {
        StartCoroutine(SetLocale("es"));
        gameDataManager.CurrentData.languageCode = "es";
        gameDataManager.SaveData();
    }

    IEnumerator SetLocale(string code)
    {
        // Wait for localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        var locale = LocalizationSettings.AvailableLocales.Locales
            .Find(l => l.Identifier.Code == code);

        if (locale != null)
            LocalizationSettings.SelectedLocale = locale;
    }
}
