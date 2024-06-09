using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private int moving;
    private Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        moving = playerMovement.moving;
        anim.SetInteger("moving", moving);
    }
}
