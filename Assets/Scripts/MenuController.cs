using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject creditsHolder;
    public GameObject buttonsHolder;        
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
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
}
