using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int currency;
    public int medkits;
    public List<string> completedStages = new();
    public List<string> unlockedPrimaries = new();
    public List<string> unlockedSecondaries = new();
    public List<string> unlockedMelee = new();
    public string equippedPrimary = "";
    public string equippedSecondary = "";
    public string equippedMelee = "";
    public string languageCode = "";

    public int destroyersKilled;
    public int dronesKilled;
    public int swordsmenKilled;
    public int infantrymenKilled;
    public int wheeliesKilled;
    public int cannonsKilled;

    // May use in the future
    public List<WeaponUpgradeData> weaponUpgradeLevels = new();
}

[System.Serializable]
public class WeaponUpgradeData
{
    public string weaponName;
    public int upgradeLevel;
}
