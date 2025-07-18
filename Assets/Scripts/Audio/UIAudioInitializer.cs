using UnityEngine;

public class UIAudioInitializer : MonoBehaviour
{
    public AudioClip clickSound;

    void Awake()
    {
        UIButtonClickAudio.SetupSharedSound(clickSound);
    }
}
