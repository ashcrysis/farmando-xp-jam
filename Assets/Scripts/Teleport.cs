using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject teleportPoint;
    public bool trigger = false;

    void LateUpdate()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.E)){
                GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = teleportPoint.transform.position;
        }

        if (trigger){
             GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = teleportPoint.transform.position;
             trigger = false;
        }
    }
}
