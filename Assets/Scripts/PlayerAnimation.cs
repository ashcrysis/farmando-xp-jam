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
    public bool playJumpOnce = true;
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
        if (playerMovement.accessJump && playJumpOnce)
        {
            Debug.Log("Jump condition met, playing jump audio.");
            jumpAudio.Play();
            playJumpOnce = false;
        }
        
        if (!playerMovement.isJumping && playerMovement.IsGrounded())
        {
            Debug.Log("Player is grounded and not jumping, resetting playJumpOnce.");
            playJumpOnce = true;
        }
    }
}
