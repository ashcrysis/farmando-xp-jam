using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject creditsHolder;
    public GameObject buttonsHolder;        
    public GameObject fadeImage;
    public float fadeDuration = 1f; 
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(FadeAndLoadScene(1));
    }
        public void Continue()
    {
        if (PlayerPrefs.HasKey("LastBonfireID"))
        {
            SceneManager.LoadScene(1);
        }
    }
    public void switchView()
    {
        if (buttonsHolder.activeSelf)
        {
            buttonsHolder.SetActive(false);
            creditsHolder.SetActive(true);
        }
        else if (creditsHolder.activeSelf)
        {
            creditsHolder.SetActive(false);
            buttonsHolder.SetActive(true);
        }
    }
   IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        fadeImage.SetActive(true);
        float elapsedTime = 0f;
        Color color = fadeImage.GetComponent<Image>().color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.GetComponent<Image>().color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.GetComponent<Image>().color = color;

        SceneManager.LoadScene(sceneIndex);
    }
}
