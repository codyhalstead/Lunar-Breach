using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAudioTrack : MonoBehaviour
{
    private static MainMenuAudioTrack instance;
    private AudioSource audioSource;
    private float fadeDuration = 1.5f;
    private string[] validScenes = { "MainMenu", "ItemShop" };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.spatialBlend = 0f;
            audioSource.volume = 0f;
            audioSource.Play();
            StartCoroutine(FadeIn());
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool isValid = false;
        foreach (string validScene in validScenes)
        {
            if (scene.name == validScene)
            {
                isValid = true;
                break;
            }
        }

        if (!isValid)
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    IEnumerator FadeIn()
    {
        float targetVolume = 0.4f;
        float startVolume = 0f;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOutAndDestroy()
    {
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        Destroy(gameObject);
    }
}