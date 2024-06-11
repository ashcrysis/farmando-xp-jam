using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IdleWatcher : MonoBehaviour
{    
    private PlayerMovement playerMovement;
    private bool idling = false;
    public float idleTimer = 0f;
    public float idleThreshold = 15f;
    public GameObject[] idleDialogos;
    [SerializeField] private GameObject[] timeDialogos;
    private float tolerance = 1f;
      private int lastIdleIndex = -1;
      public  bool gameStarted = false;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        gameStarted = PlayerPrefs.GetInt("gameStarted") == 1 ? true:false;
        if (gameStarted)
        {
            GameObject.FindGameObjectWithTag("Timer").GetComponent<TMP_Text>().enabled = true;
            GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().enabled = true;
        }
    }

void Update()
    {

        if (!gameStarted)
            {            
                return;
            }
        if (GameObject.FindGameObjectWithTag("Timer") != null){
       
        float timeRemaining = GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().timeRemaining;
        
        if (Mathf.Approximately(timeRemaining, 90) || Mathf.Abs(timeRemaining - 90) <= tolerance)
        {
            if (!timeDialogos[0].activeSelf)
            {
                timeDialogos[0].SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetInteger("moving",0);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isDashing",false);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isRunning",false);
            }
        }
    }

           if (playerMovement.moving == 1)
    {
        idleTimer = 0f;
        idling = false; 
    }
    else
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleThreshold && !idling)
            {
                idling = true;
                if (idleDialogos.Length > 0)
                {
                    int randomIndex;
                    do
                    {
                        randomIndex = Random.Range(0, idleDialogos.Length);
                    } while (randomIndex == lastIdleIndex);

                    if (!idleDialogos[randomIndex].activeSelf)
                    {
                        idleDialogos[randomIndex].SetActive(true);
                        lastIdleIndex = randomIndex; 
                        idleTimer = 0f;
                        idling = false; 
                    }
                }
            }
        }
    
    }
}

