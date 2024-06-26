using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject teleportPoint;
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public float recognitionDelay = 2.5f;

    private bool canRecognizeKey = true;

    void LateUpdate()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) && canRecognizeKey)
        {
            if (!IsDialogueActive())
            {
                StartCoroutine(RecognizeKeyWithDelay());
            }
        }
    }

    IEnumerator RecognizeKeyWithDelay()
    {
        canRecognizeKey = false; 
        GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<DashTrail>().SetEnabled(false);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetInteger("moving", 0);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isDashing", false);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isRunning", false);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetInteger("moving", 0);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isDashing", false);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isRunning", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioController>().DisableAllAudio();
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isJumping",false);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isFalling",false);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isJumping",false);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isFalling",false);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("reset",true);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("reset",true);
        StartCoroutine(FadeAndTeleport());
        yield return new WaitForSeconds(recognitionDelay);
        canRecognizeKey = true; 

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

        GameObject.FindGameObjectWithTag("Player").transform.position = teleportPoint.transform.position;
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeImage(0f));
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioController>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("reset",false);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("reset",false);
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
    bool IsDialogueActive()
    {
        GameObject[] dialogueObjects = GameObject.FindGameObjectsWithTag("dialogue");
        foreach (GameObject obj in dialogueObjects)
        {
            if (obj.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
