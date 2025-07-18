using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopUpUI : MonoBehaviour
{
    public GameObject achievementUI;
    public AudioClip achievementNotificationSound;
    [SerializeField] private AudioSource uiAudioSource;
    public Image image;

    public Sprite destroyerImage;
    public Sprite dronesImage;
    public Sprite swordsmanImage;
    public Sprite infantrymanImage;
    public Sprite wheelieImage;
    public Sprite cannonImage;

    public Sprite level1Image;
    public Sprite level2Image;
    public Sprite level3Image;

    public static string DestroyerAchievement = "DestroyerAchievement";
    public static string DroneAchievement = "DroneAchievement";
    public static string SwordsmanAchievement = "SwordsmanAchievement";
    public static string InfantrymanAchievement = "InfantrymanAchievement";
    public static string WheelieAchievement = "WheelieAchievement";
    public static string CannonAchievement = "CannonAchievement";
    public static string Level1Achievement = "Stage1";
    public static string Level2Achievement = "Stage2";
    public static string Level3Achievement = "Stage3";

    public void launchAchievement(string achievementKey)
    {
        SetAchievementImage(achievementKey);
        if (uiAudioSource != null && achievementNotificationSound != null)
        {
            uiAudioSource.PlayOneShot(achievementNotificationSound);
        }
        StartCoroutine(DisplayAchievementForSeconds(3f));
    }

    private void SetAchievementImage(string achievementKey)
    {
        switch (achievementKey)
        {
            case var key when key == DestroyerAchievement:
                image.sprite = destroyerImage;
                break;

            case var key when key == DroneAchievement:
                image.sprite = dronesImage;
                break;

            case var key when key == SwordsmanAchievement:
                image.sprite = swordsmanImage;
                break;

            case var key when key == InfantrymanAchievement:
                image.sprite = infantrymanImage;
                break;

            case var key when key == WheelieAchievement:
                image.sprite = wheelieImage;
                break;

            case var key when key == CannonAchievement:
                image.sprite = cannonImage;
                break;

            case var key when key == Level1Achievement:
                image.sprite = level1Image;
                break;

            case var key when key == Level2Achievement:
                image.sprite = level2Image;
                break;

            case var key when key == Level3Achievement:
                image.sprite = level3Image;
                break;

            default:
                
                break;
        }
    }

    private IEnumerator DisplayAchievementForSeconds(float duration)
    {
        achievementUI.SetActive(true);
        yield return new WaitForSeconds(duration);
        achievementUI.SetActive(false);
    }

}
