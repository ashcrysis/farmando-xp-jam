using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseController : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseCanvas;
    private Vector2 storedVelocity;
    public void pause()
    {
        paused = !paused;
        UpdatePauseState();
    }

    private void UpdatePauseState()
    {
        pauseCanvas.SetActive(paused);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject timer = GameObject.FindGameObjectWithTag("Timer");
        var enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (paused)
        {
            if (timer != null)
            {
                timer.GetComponent<CountdownTimer>().enabled = false;
            }

            if (player != null)
            {
                storedVelocity = player.GetComponent<Rigidbody2D>().velocity;
                player.GetComponent<Rigidbody2D>().simulated = false;
                
                player.GetComponent<DashTrail>().SetEnabled(false);
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
                player.GetComponent<PlayerAnimation>().enabled = false;
                player.GetComponent<Dash>().enabled = false;
                player.GetComponentInChildren<Animator>().enabled = false;
                player.GetComponentsInChildren<Animator>()[1].enabled = false;
                player.GetComponent<AudioController>().DisableAllAudio();
                player.GetComponentInChildren<IdleWatcher>().enabled = false;
                player.GetComponentInChildren<IfritAutoDialogue>().enabled = false;
            }

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enabled = false;
                enemy.GetComponentInChildren<Animator>().enabled = false;
                enemy.GetComponentsInChildren<Animator>()[1].enabled = false;
            }
        }
        else
        {
            if (timer != null)
            {
                timer.GetComponent<CountdownTimer>().enabled = true;
            }

            if (player != null)
            {
                player.GetComponent<DashTrail>().SetEnabled(true);
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<PlayerAnimation>().enabled = true;
                player.GetComponent<Dash>().enabled = true;
                player.GetComponentInChildren<IdleWatcher>().enabled = true;
                player.GetComponentInChildren<IfritAutoDialogue>().enabled = true;
                player.GetComponent<Rigidbody2D>().simulated = true;
                player.GetComponent<Rigidbody2D>().velocity = storedVelocity;
                player.GetComponentInChildren<Animator>().enabled = true;
                player.GetComponentsInChildren<Animator>()[1].enabled = true;
            }

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enabled = true;
                enemy.GetComponentInChildren<Animator>().enabled = true;
                enemy.GetComponentsInChildren<Animator>()[1].enabled = true;
            }
        }
    }

  bool IsDialogueActive()
{
    GameObject[] dialogueObjects = GameObject.FindGameObjectsWithTag("dialogue");
    GameObject[] starterDialogueObjects = GameObject.FindGameObjectsWithTag("starterdialogue");

    GameObject[] allDialogueObjects = new GameObject[dialogueObjects.Length + starterDialogueObjects.Length];

    dialogueObjects.CopyTo(allDialogueObjects, 0);
    starterDialogueObjects.CopyTo(allDialogueObjects, dialogueObjects.Length);

    foreach (GameObject obj in allDialogueObjects)
    {
        if (obj.activeInHierarchy)
        {
            return true;
        }
    }
    return false;
}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsDialogueActive())
        {
            pause();
        }
    }
}
