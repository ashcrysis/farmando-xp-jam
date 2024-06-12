using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource deathSound;
    public AudioSource runAudio;
    public AudioSource walkAudio;
    public AudioSource idleBreathingAudio;
    public bool onlyPlayDeathOnce = true;

    private bool isWalkingAudioPlaying = false;
    private bool isRunningAudioPlaying = false;
    private bool isIdleBreathingPlaying = false;

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
            isIdleBreathingPlaying = false;
            runAudio.Stop();
            idleBreathingAudio.Stop();
        }
        else if (isMoving && isRunning && !isRunningAudioPlaying && playerMovement.IsGrounded())
        {
            // Start running audio
            runAudio.Play();
            runAudio.loop = true;
            isRunningAudioPlaying = true;
            isWalkingAudioPlaying = false;
            isIdleBreathingPlaying = false;
            walkAudio.Stop();
            idleBreathingAudio.Stop();
        }
        else if (!isMoving && !GetComponent<DeathCounter>().isDying && !isIdleBreathingPlaying && playerMovement.IsGrounded())
        {
            // Start idle breathing audio
            idleBreathingAudio.Play();
            idleBreathingAudio.loop = true;
            isIdleBreathingPlaying = true;
            isWalkingAudioPlaying = false;
            isRunningAudioPlaying = false;
            walkAudio.Stop();
            runAudio.Stop();
        }
        else if (!isMoving || GetComponent<DeathCounter>().isDying)
        {
            // Stop all audio when not moving or dying
            walkAudio.Stop();
            runAudio.Stop();
            if (GetComponent<DeathCounter>().isDying)
            {
                idleBreathingAudio.Stop();
                isIdleBreathingPlaying = false;
            }
            isWalkingAudioPlaying = false;
            isRunningAudioPlaying = false;
        }
    }
}
