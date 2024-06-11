using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 300f;
    public float timeRemaining;

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
            SceneManager.LoadScene(0);    
}

        // Calculate hours, minutes, and seconds
        int hours = (int)(timeRemaining / 3600);
        int minutes = (int)((timeRemaining % 3600) / 60);
        int seconds = (int)(timeRemaining % 60);

        string timerString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        GetComponent<TMP_Text>().text = timerString;
    }
}
