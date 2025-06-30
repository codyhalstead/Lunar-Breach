using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public TMP_Text completedMessage;
    public float fadeDuration = 1f;
    public Boolean missionComplete = false;

    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = alpha;
            fadeImage.color = c;
            if (completedMessage != null && missionComplete)
            {
                Color msgColor = completedMessage.color;
                msgColor.a = alpha;
                completedMessage.color = msgColor;
            }
            yield return null;
        }
        yield return new WaitForSeconds(fadeDuration);
    }
}