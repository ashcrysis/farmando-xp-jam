using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class ZoomOutCamera : MonoBehaviour
{
    public PixelPerfectCamera pixelPerfectCamera;
    public float desiredPPU = 10f; 
    public float fadeDuration = 0.5f; 
    public Image fadeImage;

    private float originalPPU; 
    private bool isFading = false;

    void Start()
    {
        if (pixelPerfectCamera == null)
        {
            Debug.LogError("Pixel Perfect Camera reference is not set.");
        }

        if (fadeImage == null)
        {
            Debug.LogError("Fade Image reference is not set.");
        }
   

        originalPPU = pixelPerfectCamera.assetsPPU;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFading)
        {
            StartCoroutine(HandleZoom(true));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!isFading)
        {
            StartCoroutine(HandleZoom(false));
        }
    }

    IEnumerator HandleZoom(bool zoomIn)
    {
        isFading = true;

        yield return new WaitForEndOfFrame();

        // Fade to 1f opacity
        yield return StartCoroutine(FadeImage(1f));

        if (fadeImage.color.a == 1f)
        {
            if (zoomIn)
            {
                pixelPerfectCamera.assetsPPU = (int)desiredPPU;
            }
            else
            {
                pixelPerfectCamera.assetsPPU = (int)originalPPU;
            }
        }
            yield return new WaitForSeconds(0.1f);

        // Fade to 0f opacity
        yield return StartCoroutine(FadeImage(0f));

        isFading = false;
    }

    IEnumerator FadeImage(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        Color finalColor = fadeImage.color;
        finalColor.a = targetAlpha;
        fadeImage.color = finalColor;

    
    }
}
