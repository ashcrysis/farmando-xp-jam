using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private int moving;
    private Animator[] animators;
    private Dash dash;

    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
    }

    void Update()
    {
        moving = playerMovement.moving;
        foreach (Animator anim in animators)
        {
            anim.SetInteger("moving", moving);
            anim.SetBool("isDashing", dash.isDashing);
            anim.SetBool("isRunning", playerMovement.isRunning);
        }
    }
}
