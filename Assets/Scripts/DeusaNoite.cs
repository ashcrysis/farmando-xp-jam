using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeusaNoite : MonoBehaviour
{
    public GameObject dialogo;
    private bool dialogueActive = false;

    void Update()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) && PlayerPrefs.GetInt("endgame") == 1)
        {
            dialogo.SetActive(true);
            dialogueActive = true; 
        }

        if (dialogueActive && !dialogo.activeSelf)
        {
            SceneManager.LoadScene(0);
        }
    }
}
