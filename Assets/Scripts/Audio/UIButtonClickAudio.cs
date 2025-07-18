using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIButtonClickAudio : MonoBehaviour, IPointerClickHandler
{
    private static AudioSource audioSource;
    private static AudioClip clickSound;

    public static void SetupSharedSound(AudioClip clickSound)
    {
        // Create only one AudioSource for all UI clicks
        if (audioSource == null)
        {
            GameObject obj = new GameObject("UIAudio");
            obj.tag = "UIAudio";
            audioSource = obj.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f;
            audioSource.volume = 0.4f;
            DontDestroyOnLoad(obj);
        }

        UIButtonClickAudio.clickSound = clickSound;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}