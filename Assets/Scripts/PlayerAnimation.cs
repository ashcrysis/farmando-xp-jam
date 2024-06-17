using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private int moving;
    private Animator[] animators;
    private Dash dash;
    private bool playJumpOnce = true;
    private bool playLandOnce = true;
    public AudioSource jumpAudio;

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
            anim.SetBool("isJumping", playerMovement.isJumping); 
            anim.SetBool("isFalling", playerMovement.isFalling);
            anim.SetBool("isGrounded", playerMovement.IsGrounded());
        }
        
        if (playerMovement.jump && playJumpOnce)
        {
            jumpAudio.Play();
            playJumpOnce = false;
        }
        
        if (playerMovement.canJump && playerMovement.IsGrounded())
        {
            playJumpOnce = true;
        }
    }
}
