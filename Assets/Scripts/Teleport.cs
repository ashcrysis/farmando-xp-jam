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
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.y);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetInteger("moving",0);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isDashing",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isRunning",false);
            
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = true;
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
