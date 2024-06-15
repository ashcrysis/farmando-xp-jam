using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 300f;
    public float timeRemaining;
    public AudioSource overAudio;
    private bool playOnce = false;
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    void Start()
    {
        
        if (PlayerPrefs.HasKey("remainingTime"))
        {
            timeRemaining = PlayerPrefs.GetFloat("remainingTime");
        }
        else
        {
            timeRemaining = totalTime;
        }
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0)
        {
            timeRemaining = 0;
            if (!playOnce){
            playOnce = true;
            StartCoroutine(whiteToMenu(1f));
            }

}

        int hours = (int)(timeRemaining / 3600);
        int minutes = (int)((timeRemaining % 3600) / 60);
        int seconds = (int)(timeRemaining % 60);

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        GetComponent<TMP_Text>().text = timerString;
    }
    private IEnumerator whiteToMenu(float delay)
    {
        overAudio.Play();

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
