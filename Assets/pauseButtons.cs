using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtons : MonoBehaviour
{
    public void retornar()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<pauseController>().pause();
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
