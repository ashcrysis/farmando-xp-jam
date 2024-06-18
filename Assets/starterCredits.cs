using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class starterCredits : MonoBehaviour
{
    private bool doOnce = true;
    private bool cutscenePlay = false;
    public void CutsceneOver()
    {
        cutscenePlay = true;
    }
    void Update()
    {
        if (cutscenePlay && doOnce)
        {
            SceneManager.LoadScene(1);
            cutscenePlay = false;
        }
    }
}
