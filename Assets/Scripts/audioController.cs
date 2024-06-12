using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource deathSound;
    public AudioSource runAudio;
    public AudioSource walkAudio;
    public bool onlyPlayDeathOnce = true;
    
    private bool isWalkingAudioPlaying = false;
    private bool isRunningAudioPlaying = false;

    void Update()
    {
        var playerMovement = GetComponent<PlayerMovement>();
        var isMoving = playerMovement.moving == 1;
        var isRunning = playerMovement.isRunning;

        if (onlyPlayDeathOnce && GetComponent<DeathCounter>().isDying)
        {
            deathSound.Play();
            onlyPlayDeathOnce = false;
        }

        if (isMoving && !isRunning && !isWalkingAudioPlaying && playerMovement.IsGrounded())
        {
            // Start walking audio
            walkAudio.Play();
            walkAudio.loop = true;
            isWalkingAudioPlaying = true;
            isRunningAudioPlaying = false;
            runAudio.Stop();
        }
        else if (isMoving && isRunning && !isRunningAudioPlaying && playerMovement.IsGrounded())
        {
            // Start running audio
            runAudio.Play();
            runAudio.loop = true;
            isRunningAudioPlaying = true;
            isWalkingAudioPlaying = false;
            walkAudio.Stop();
        }
        else if (!isMoving || GetComponent<DeathCounter>().isDying)
        {
            // Stop all audio when not moving
            walkAudio.Stop();
            runAudio.Stop();
            isWalkingAudioPlaying = false;
            isRunningAudioPlaying = false;
        }
    }
}
