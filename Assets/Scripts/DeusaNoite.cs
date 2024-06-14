using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeusaNoite : MonoBehaviour
{
    public GameObject dialogo;
    public Image fadeImage;
    public float fadeDuration = 1f; 
    private bool dialogueActive = false;

    void Update()
    {
        if (PlayerPrefs.GetInt("endgame") == 1)
        {
            GetComponent<DialoguePlayer>().enabled = false;
        }
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) && PlayerPrefs.GetInt("endgame") == 1)
        {
            dialogo.SetActive(true);
            dialogueActive = true; 
        }

        if (dialogueActive && !dialogo.activeSelf)
        {
            StartCoroutine(FadeAndLoadScene(0));
        }
    }

    IEnumerator FadeAndLoadScene(int sceneIndex)
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

        SceneManager.LoadScene(sceneIndex);
    }
}
