using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfritAutoDialogue : MonoBehaviour
{
    private DeathCounter counter;
    [SerializeField] private GameObject[] dialogos;
    [SerializeField] private GameObject[] timeDialogos;
    [SerializeField] private int[] deathNeeded;
    private bool idling = false;
    private float tolerance = 1f;
    private PlayerMovement playerMovement;
    public float idleTimer = 0f;
    public GameObject[] idleDialogos;
    public float idleThreshold = 15f;

    
void Start()
{
    counter = GetComponentInParent<DeathCounter>();
    playerMovement = GetComponentInParent<PlayerMovement>();   
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
            PlayerPrefs.SetInt("Dialogue_" + index, 1);
        }
    }
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


