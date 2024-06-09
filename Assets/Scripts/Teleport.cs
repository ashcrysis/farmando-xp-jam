using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject teleportPoint;
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    void LateUpdate()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeAndTeleport());
        }
    }

    IEnumerator FadeAndTeleport()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }


        color.a = 1f;
        fadeImage.color = color;

        //yield return new WaitForSeconds(2f);

        GameObject.FindGameObjectWithTag("Player").transform.position = teleportPoint.transform.position;
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeImage(0f));
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
