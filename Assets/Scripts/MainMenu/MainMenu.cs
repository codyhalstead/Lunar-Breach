using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Button level2Button;
    public UnityEngine.UI.Button level3Button;
    private GameDataManager gameDataManager;

    void Start()
    {
        Cursor.visible = true;
        gameDataManager = GameDataManager.GetInstance();
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
}
