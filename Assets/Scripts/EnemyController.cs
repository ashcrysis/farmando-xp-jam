using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    public bool foundPlayer = false;
    public float enemyPatrolSpeed;
    public float enemyFollowSpeed;
    public Transform pointA;
    public Transform pointB;    
    private Rigidbody2D rb;
    private float distanceToPlayer;
    private bool movingToPointA = true;
    private bool followingPlayer = false;
    private bool playerSpawned = false;
    private bool hasRun = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && !hasRun){
            playerSpawned = true;
            player = GameObject.FindGameObjectWithTag("Player");
            hasRun = true;
        }
        
         if (playerSpawned){
        foundPlayer = GetComponentInChildren<EnemyVision>().foundPlayer;
        distanceToPlayer = Vector2.Distance(rb.position, player.transform.position);
        
        }
    }

    void FixedUpdate()
    {
        if (playerSpawned)
        {
        if (IsGrounded()){
        if ((foundPlayer || distanceToPlayer < 1 ) && !GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying)
        {
            MoveTowards(player.transform.position);

            followingPlayer = true;
        }
        else
        {
            Patrol();
            followingPlayer = false;
        }
        }
    }
    }
    public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
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
        if (!foundPlayer){
            rb.velocity = new Vector2(direction * enemyPatrolSpeed, rb.velocity.y);
        }
        else{
                rb.velocity = new Vector2(direction * enemyFollowSpeed, rb.velocity.y);
            }
        if (direction != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);

        }
    }

}
