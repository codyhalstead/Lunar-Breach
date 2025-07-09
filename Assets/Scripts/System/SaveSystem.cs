using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Localization.Settings;

public static class SaveSystem
{
    private static string path => Application.persistentDataPath + "/save.json";

    public static void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static GameData LoadGame()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        // Set defaults for new save
        GameData gameData = new GameData();
        gameData.unlockedPrimaries = new List<string> { "Primary1" };
        gameData.unlockedSecondaries = new List<string> { "SecondaryWeapon1" };
        gameData.unlockedMelee = new List<string> { "Melee1" };
        gameData.equippedPrimary = "Primary1";
        gameData.equippedSecondary = "SecondaryWeapon1";
        gameData.equippedMelee = "Melee1";
        gameData.languageCode = GetDefaultLanguage();
        gameData.swordsmenKilled = 0;
        gameData.wheeliesKilled = 0;
        gameData.infantrymenKilled = 0;
        gameData.cannonsKilled = 0;
        gameData.destroyersKilled = 0;
        gameData.dronesKilled = 0;
        SaveGame(gameData);
        return gameData; 
    }

    private static string GetDefaultLanguage()
    {
        SystemLanguage sysLang = Application.systemLanguage;
        return GetCodeFromSystemLanguage(sysLang);
    }

    private static string GetCodeFromSystemLanguage(SystemLanguage systemLanguage)
    {
        switch (systemLanguage)
        {
            case SystemLanguage.English: return "en";
            case SystemLanguage.Spanish: return "es";
            // fallback
            default: return "en";
        }
    }

}