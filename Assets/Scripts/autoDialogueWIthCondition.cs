using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class autoDialogueWithCondition : MonoBehaviour
{
    [SerializeField] private GameObject dialogo;
    [SerializeField] private bool touching = false;
    [SerializeField] private bool touchedOnce = false;
    [SerializeField] private bool requireEndgameCondition = false;
    public int dialogueID;

    private string playerPrefsKey;

    void Start()
    {
        playerPrefsKey = "hasPlayed_" + dialogueID;
        touchedOnce = PlayerPrefs.GetInt(playerPrefsKey, 0) == 1;
    }

    void Update()
    {
        bool endgameConditionMet = !requireEndgameCondition || PlayerPrefs.GetInt("endgame") == 1;

        if (touching && !touchedOnce && !GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying && endgameConditionMet)
        {
            touchedOnce = true;
            dialogo.SetActive(true);

            PlayerPrefs.SetInt(playerPrefsKey, 1);
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
