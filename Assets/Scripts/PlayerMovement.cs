using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 5f;
    public float jumpingPower = 12f;
    public float speedVeloc = 20f;
    public bool isFacingRight = true;
    private float origSpeed;
    public float acceleration = 2f;
    public float fallMultiplier = 1.5f;
    public float slopeMultiplier = 35f;
    public ParticleSystem JumpParticle;
    public ParticleSystem LandParticle;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public int moving;
    private Animator anim;
    private float coyoteTime = 0.15f;
    private float coyoteTimer = 0f;
    public bool isJumping = false;
    public bool jump = false;
    public bool canJump = true;
    public float jumpDelay = 0.2f;
    private bool hasLaunched = false;

    public bool justLanded = false;
    public Stamina stamina;
    public bool isRunning = false;
    public bool onSlope;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform wallCheckLeft;
    public float slopeAngle;
    public bool isFalling = false;

    private void Start()
    {
        Initialize();

        Application.targetFrameRate = 30;
    }

    private void Initialize()
    {
        origSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentsInChildren<Animator>()[0];
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = gameObject.transform.position;
    }

    private void Update()
    {
        HandleInput();
        Flip();
        animatorHandler();
        UpdateLandingState();
        if (stamina.stamina > stamina.staminaDashValue)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
            {
                if (IsGrounded())
                {
                    jump = true;
                    
                }
            }
        }
        if (!IsGrounded() && rb.velocity.y < 0)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        moving = Mathf.Abs(horizontal) > 0 ? 1 : 0;
        if (IsGrounded())
        {
            if (!isRunning && !Input.GetKey(KeyCode.Z))
            {
                stamina.Actions(2);
            }
        }
        AdjustSpeed();
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheckLeft.position, 0.1f, groundLayer) || Physics2D.OverlapCircle(wallCheckRight.position, 0.1f, groundLayer);
    }

    private void UpdateLandingState()
    {
        if (IsGrounded() && rb.velocity.y <= 0)
        {
            if (!justLanded)
            {
                // LandParticle.Play();
                justLanded = true;
            }
        }
    }

    private void Jump()
    {
        if (jump)
        {
            isJumping = true;
            stamina.Actions(3);
            justLanded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            StartCoroutine(ResetCoyoteJumpAfterDelay(coyoteTime));
            canJump = false;
            StartCoroutine(JumpDelay(jumpDelay));
            jump = false;
        }
    }

    private void AdjustSpeed()
    {
        if (Input.GetKey(KeyCode.Z) && moving != 0 && stamina.stamina > stamina.staminaRunValue && IsGrounded())
        {
            isRunning = true;
            if (onSlope && IsGrounded())
            {
                speed = speedVeloc + (4f + slopeAngle / 35f);
            }
            else
            {
                speed = speedVeloc;
            }
            stamina.Actions(0);
        }
        else
        {
            isRunning = false;
            if (!onSlope)
            {
                speed = origSpeed;
            }
        }
        if (moving == 0 && !GetComponent<Dash>().isDashing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (isFalling){
            isJumping = false;
        }
    }



    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
    }

    private void MovePlayer()
    {
        float targetVelocityX = horizontal * speed;
        float smoothVelocityX = Mathf.Lerp(rb.velocity.x, targetVelocityX, Time.deltaTime * acceleration);

        rb.AddForce(new Vector2(smoothVelocityX - rb.velocity.x, 0), ForceMode2D.Impulse);

        HandleGroundedState();
        HandleSlopeAdjustment();
    }

    private void HandleSlopeAdjustment()
    {
        if (IsGrounded())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, groundLayer);
            if (hit.collider != null)
            {
                slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeAngle > 0 && slopeAngle < 90)
                {
                    float direction = hit.normal.x > 0 ? 1 : -1;
                    float rotationSpeed = 1f;
                    float targetRotation = direction * -slopeAngle;
                    float smoothRotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetRotation, Time.deltaTime * rotationSpeed);
                    transform.rotation = Quaternion.Euler(0, 0, smoothRotation);
                    onSlope = true;
                    speed = origSpeed * (1f + slopeAngle / slopeMultiplier);
                }
                else
                {
                    onSlope = false;
                    float resetRotationSpeed = 3f;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * resetRotationSpeed);
                }
            }
        }
        else
        {
            // Smoothly reset rotation if not grounded
            float resetRotationSpeed = 3f; // Adjust the reset rotation speed factor as needed
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * resetRotationSpeed);
        }
    }

    private void HandleGroundedState()
    {
        if (IsGrounded())
        {
            coyoteTimer = coyoteTime;

            hasLaunched = false;
        }
        else if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.fixedDeltaTime;
        }
    }

    private void animatorHandler()
    {
        anim.SetBool("isJumping", isJumping);
        if (isJumping && !hasLaunched)
        {
            // JumpParticle.Play();
            hasLaunched = true;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private IEnumerator ResetCoyoteJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator JumpDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canJump = true;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            GetComponentInChildren<SpriteRenderer>().flipX = !isFacingRight;
            GetComponentsInChildren<SpriteRenderer>()[1].flipX = isFacingRight;
        }
    }

    private IEnumerator ResetJustLandedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        justLanded = false;
    }
}
