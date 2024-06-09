using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject teleportPoint;

    void Update()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.E)){
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = teleportPoint.transform.position;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}
