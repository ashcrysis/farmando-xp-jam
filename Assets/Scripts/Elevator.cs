using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform starterPosition;
    public Transform endPosition;
    private Rigidbody2D rb;
    public float elevatorSpeed;
    public Transform destiny;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destiny = starterPosition;
    }

    void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, destiny.position, elevatorSpeed * Time.fixedDeltaTime));
    }
}
