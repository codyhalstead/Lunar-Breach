using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAudioTrack : MonoBehaviour
{
    private AudioSource audioSource;
    private float fadeInDuration = 1.5f;
    private float fadeOutDuration = 0.5f;
    private static LevelAudioTrack instance;
    private string originalScene;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0f;
        originalScene = SceneManager.GetActiveScene().name;
        StartCoroutine(FadeIn());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        audioSource.Play();
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != originalScene)
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    IEnumerator FadeIn()
    {
        float targetVolume = 0.1f;
        float startVolume = 0f;
        float t = 0f;

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeInDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOutAndDestroy()
    {
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            yield return null;
        }

        Destroy(gameObject);
    }
}