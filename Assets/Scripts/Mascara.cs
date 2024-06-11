using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.GetInt("endgame") == 1)
        {
             Destroy(gameObject);
        }
    }
    void Update()
    {
        if (GetComponent<Interavel>().canInteract && !GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying && Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetInt("endgame",1);
            Destroy(gameObject);
        }
    }
}
