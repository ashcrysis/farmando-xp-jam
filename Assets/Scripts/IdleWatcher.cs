using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWatcher : MonoBehaviour
{    
    private PlayerMovement playerMovement;
    private bool idling = false;
    public float idleTimer = 0f;
    public GameObject[] idleDialogos;
    public float idleThreshold = 15f;
    [SerializeField] private GameObject[] timeDialogos;
    private float tolerance = 1f;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

void Update()
    {
        float timeRemaining = GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().timeRemaining;
        
        if (Mathf.Approximately(timeRemaining, 90) || Mathf.Abs(timeRemaining - 90) <= tolerance)
        {
            if (!timeDialogos[0].activeSelf)
            {
                timeDialogos[0].SetActive(true);
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
                int randomIndex = Random.Range(0, idleDialogos.Length);
                if (!idleDialogos[randomIndex].activeSelf)
                {
                    idleDialogos[randomIndex].SetActive(true);
                }
            }
        }
    }
    }
}