using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;
    private bool hasRest = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        hasRest = PlayerPrefs.GetInt("hasRest") == 1 ? true : false;
        if (hasRest){
            isOpen = true;
        }
        if (PlayerPrefs.GetInt("hasKey") == 1  && GetComponent<Interavel>().canInteract && GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying == false && Input.GetKeyDown(KeyCode.C) && !hasRest )
        {
            isOpen = true;
        }   
        if (PlayerPrefs.GetInt("endgame") == 1 && !hasRest)
        {
            isOpen = false;
            GetComponent<Interavel>().enabled = false;
        }
        anim.SetBool("open", isOpen);
    }
}
