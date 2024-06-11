using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform starterPosition;
    public Transform endPosition;
    private Rigidbody2D rb;
    public float elevatorSpeed;
    private Transform destiny;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destiny = gameObject.transform;
    }

    void Update()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) && !IsDialogueActive())
            {
                float threshold = 1f;
                if (Vector3.Distance(transform.position, starterPosition.position) < threshold)
                {
                    Debug.Log("Moving to endPosition");
                    destiny = endPosition;
                }
                else if (Vector3.Distance(transform.position, endPosition.position) < threshold)
                {
                    Debug.Log("Moving to starterPosition");
                    destiny = starterPosition;
                }
            }
    }
    void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, destiny.position, elevatorSpeed * Time.fixedDeltaTime));
    }
     bool IsDialogueActive()
    {
        GameObject[] dialogueObjects = GameObject.FindGameObjectsWithTag("dialogue");
        foreach (GameObject obj in dialogueObjects)
        {
            if (obj.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
