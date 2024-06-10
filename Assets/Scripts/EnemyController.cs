using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public bool foundPlayer = false;
    public float enemySpeed;
    public Transform pointA;
    public Transform pointB;    
    private Rigidbody2D rb;
    private float distanceToPlayer;
    private bool movingToPointA = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foundPlayer = GetComponentInChildren<EnemyVision>().foundPlayer;
        distanceToPlayer = Vector2.Distance(rb.position, player.transform.position);
    }

    void FixedUpdate()
    {
        if (foundPlayer || distanceToPlayer < 1)
        {
            MoveTowards(player.transform.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (!foundPlayer){
        if (movingToPointA)
        {
            MoveTowards(new Vector2(pointA.position.x, rb.position.y));

            if (Vector2.Distance(rb.position, pointA.position) < 2f)
            {
                movingToPointA = false;
            }
        }
        else
        {
            MoveTowards(new Vector2(pointB.position.x, rb.position.y));

            if (Vector2.Distance(rb.position, pointB.position) < 2f)
            {
                movingToPointA = true;
            }
            }
        }
    }

 void MoveTowards(Vector2 target)
    {
        float direction = target.x - rb.position.x;
        direction = Mathf.Sign(direction); 

        rb.velocity = new Vector2(direction * enemySpeed, rb.velocity.y);

        if (direction != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
        }
    }

}
