using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    private GameDataManager gameDataManager;

    void Start()
    {
        gameDataManager = GameDataManager.GetInstance();
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.Destroy();
            }
            StartCoroutine(EndLevel());
        }
    }

    private IEnumerator EndLevel()
    {
        ScreenFader screenFader = FindFirstObjectByType<ScreenFader>();
        if (screenFader != null)
        {
            screenFader.missionComplete = true;
            yield return StartCoroutine(screenFader.FadeOut());
        }
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (!gameDataManager.CurrentData.completedStages.Contains(currentSceneName))
        {
            gameDataManager.CurrentData.completedStages.Add(currentSceneName);
        }
        gameDataManager.SaveData();
        SceneManager.LoadScene("MainMenu");
    }
}