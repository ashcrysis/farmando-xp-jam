using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private int moving;
    private Animator anim;
    private Dash dash;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
    }

    void Update()
    {
        moving = playerMovement.moving;
        anim.SetInteger("moving", moving);
        anim.SetBool("isDashing",dash.isDashing);
        anim.SetBool("isRunning",playerMovement.isRunning);
    }
}
