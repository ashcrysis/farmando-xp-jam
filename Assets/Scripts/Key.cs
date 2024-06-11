using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("hasKey") == 1)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C) && GetComponent<Interavel>().canInteract && !GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying)
        {
            PlayerPrefs.SetInt("hasKey",1);
            Destroy(gameObject);
        }    
    }
}
