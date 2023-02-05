using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public bool playOnStart = false;
    public bool startOpaque = false;
    public bool fading = false;

    [SerializeField] private Image image = null;

    void Start() {
        image = image == null ? GetComponent<Image>() : image;
    }

    public void FadeBlack(float time) {
        fading = true;
        StartCoroutine(FadeBlackDuringTime(time));
    }

    public void FadeTransparent(float time) {
        fading = true;
        StartCoroutine(FadeTransparentDuringTime(time));
    }

    IEnumerator FadeTransparentDuringTime(float time) {
        image.color = new Color(image.color[0], image.color[1], image.color[2], 1);
        for (float t = 0.0f; t < time; t += Time.deltaTime / (time))
        {
            Color newColor = new Color(image.color[0], image.color[1], image.color[2], Mathf.Lerp(1, 0, t));
            image.color = newColor;
            yield return null;
        }
        image.color = new Color(image.color[0], image.color[1], image.color[2], 0);
        fading = false;
    }

    IEnumerator FadeBlackDuringTime(float time) {
        image.color = new Color(image.color[0], image.color[1], image.color[2], 0);
        for (float t = 0.0f; t < time; t += Time.deltaTime / time)
        {
            Color newColor = new Color(image.color[0], image.color[1], image.color[2], Mathf.Lerp(0, 1, t));
            image.color = newColor;
            yield return null;
        }
        image.color = new Color(image.color[0], image.color[1], image.color[2], 1);
        fading = false;
    }
}
