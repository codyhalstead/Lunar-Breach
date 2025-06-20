using System.Collections.Generic;
using System;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    private static GameDataManager LoadedInstance;
    public static GameDataManager Instance => GetInstance();

    public GameData CurrentData { get; private set; }

    public List<string> allPrimaries = new List<String> { "Primary1" };
    public List<string> allSecondaries = new List<String> { "SecondaryWeapon1" };
    public List<string> allMelee = new List<String> { "Melee1" };

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
        Debug.Log("GameDataManager: AWAKE");
        if (LoadedInstance == null)
        {
            Debug.Log("GameDataManager: Instance was null, make new");
            LoadedInstance = this;
            DontDestroyOnLoad(gameObject);
            LoadData(); 
        }
        else
        {
            Debug.Log("GameDataManager: Instance was NOT null");
            Destroy(gameObject);
        }
    }

    public void LoadData()
    {
        Debug.Log("GameDataManager: Trying To Load Data");
        CurrentData = SaveSystem.LoadGame();
    }

    public void SaveData()
    {
        Debug.Log("GameDataManager: Trying To Save Data");
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