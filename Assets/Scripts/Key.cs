using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool hasCollectedKey = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("hasKey") == 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && GetComponent<Interavel>().canInteract && !GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying && !hasCollectedKey)
        {
            hasCollectedKey = true;
            StartCoroutine(DelayedSetKey());
        }
    }

    private IEnumerator DelayedSetKey()
    {
        yield return new WaitForSeconds(5);

        // Check if the player is still alive after 5 seconds
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying)
        {
            PlayerPrefs.SetInt("hasKey", 1);
        }

        Destroy(gameObject);
    }
}