using System.Collections.Generic;
using System;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    private static GameDataManager LoadedInstance;
    public static GameDataManager Instance => GetInstance();

    public GameData CurrentData { get; private set; }

    public List<string> allPrimaries = new List<String> { "Primary1", "Primary2", "Primary3", "Primary4" };
    public List<string> allSecondaries = new List<String> { "SecondaryWeapon1", "SecondaryWeapon2", "SecondaryWeapon3", "SecondaryWeapon4" };
    public List<string> allMelee = new List<String> { "Melee1", "Melee2", "Melee3", "Melee4" };

    private static bool applicationIsQuitting = false;

    public static GameDataManager GetInstance()
    {
        if (LoadedInstance == null && !applicationIsQuitting)
        {
            GameObject gObject = new GameObject("GameDataManager");
            LoadedInstance = gObject.AddComponent<GameDataManager>();
            DontDestroyOnLoad(gObject);
            LoadedInstance.LoadData();
        }
        return LoadedInstance;
    }

    private void Awake()
    {
        if (LoadedInstance == null)
        {
            LoadedInstance = this;
            DontDestroyOnLoad(gameObject);
            LoadData(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadData()
    {
        CurrentData = SaveSystem.LoadGame();
    }

    public void SaveData()
    {
        SaveSystem.SaveGame(CurrentData);
    }

    public void AddCurrency(int amount)
    {
        CurrentData.currency += amount;
        SaveData();
    }

    private void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
}