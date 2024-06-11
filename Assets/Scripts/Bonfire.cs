using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public int ID;
    public Vector2 position;

    private SaveManager saveManager;

    void Start()
    {
        position = transform.position;
        saveManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveManager>();
    }

    void Update()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) &&  GetComponentInParent<DialoguePlayer>().isPlaying)
        {
            saveManager.lastBonfireID = ID;
            saveManager.lastBonfirePosition = position;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>().stamina =  GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>().MaxStamina;
            saveManager.Save();
        }
    }
}
