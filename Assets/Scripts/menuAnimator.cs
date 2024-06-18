using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAnimator : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();    
        anim.SetBool("hasSave",PlayerPrefs.HasKey("LastBonfireID"));
        
    }

}
