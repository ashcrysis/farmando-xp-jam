using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class autoDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogo;
    [SerializeField] private bool touching = false;
    [SerializeField] private bool touchedOnce = false;

    private string playerPrefsKey; // Key to save the state for each dialogue

    void Start()
    {
        playerPrefsKey = "hasPlayed_" + gameObject.GetInstanceID();
        touchedOnce = PlayerPrefs.GetInt(playerPrefsKey, 0) == 1;
    }

    void Update()
    {
        if (touching && !touchedOnce)
        {
            touchedOnce = true;
            dialogo.SetActive(true);

            PlayerPrefs.SetInt(playerPrefsKey, 1);
            PlayerPrefs.Save();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            touching = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            touching = false;
        }
    }
}
