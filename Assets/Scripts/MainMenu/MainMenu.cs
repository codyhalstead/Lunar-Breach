using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
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
