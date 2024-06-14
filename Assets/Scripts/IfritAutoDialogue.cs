using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfritAutoDialogue : MonoBehaviour
{
    private DeathCounter counter;
    [SerializeField] private GameObject[] dialogos;
    [SerializeField] private int[] deathNeeded;

    
void Start()
{
    counter = GetComponentInParent<DeathCounter>();
    for (int i = 0; i < dialogos.Length; i++)
    {
        if (PlayerPrefs.HasKey("Dialogue_" + i))
        {
            if (PlayerPrefs.GetInt("Dialogue_" + i) == 1)
            {
                dialogos[i].SetActive(false);
            }
        }
    }

    int index = -1;
    for (int i = 0; i < deathNeeded.Length; i++)
    {
        if (counter.deathCounter >= deathNeeded[i])
        {
            index = i;
        }
    }

    if (index >= 0 && index < dialogos.Length)
    {
        if (!PlayerPrefs.HasKey("Dialogue_" + index))
        {
            dialogos[index].SetActive(true);
           /* 
            GameObject.FindGameObjectWithTag("Player").GetComponent<DashTrail>().SetEnabled(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.y);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetInteger("moving",0);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isDashing",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isRunning",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetInteger("moving",0);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isDashing",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isRunning",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isJumping",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isFalling",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isJumping",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isFalling",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("reset",true);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[0].SetBool("reset",true);
            */
            PlayerPrefs.SetInt("Dialogue_" + index, 1);
        }
    }
}

}


