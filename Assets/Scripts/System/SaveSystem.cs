using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
        SaveGame(gameData);
        return gameData; 
    }
}