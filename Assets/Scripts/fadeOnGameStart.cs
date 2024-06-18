using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeOnGameStart : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    void Awake()
    {
        if (!PlayerPrefs.HasKey("gameStarted")){
        StartCoroutine(FadeImage(0f));    
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
     IEnumerator FadeImage(float targetAlpha)
    {
        float startAlpha = gameObject.GetComponent<Image>().color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color color = gameObject.GetComponent<Image>().color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            gameObject.GetComponent<Image>().color = color;
            yield return null;
        }

        Color finalColor = gameObject.GetComponent<Image>().color;
        finalColor.a = targetAlpha;
        gameObject.GetComponent<Image>().color = finalColor;
        gameObject.SetActive(false);
    }
}
