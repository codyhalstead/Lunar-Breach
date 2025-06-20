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
        Debug.Log("GameDataManager: Attempting to load game data...");
        if (File.Exists(path))
        {
            Debug.Log("GameDataManager: Data Found");
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        Debug.Log("GameDataManager: Data Not Found, Make new");
        return new GameData(); 
    }
}