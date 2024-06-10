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
    public ParticleSystem JumpParticle;
    public ParticleSystem LandParticle;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public int moving;
    private Animator anim;
    private bool canCoyoteJump = false;
    private float coyoteTime = 0.15f;
    private float coyoteTimer = 0f;
    private bool isJumping = false;
    private bool hasLaunched = false;
    private bool justLanded = false;
    public Stamina stamina;
    public bool isRunning = false;
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        origSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = gameObject.transform.position;
    }

    private void Update()
    {
        HandleInput();
        Flip();
        animatorHandler();
        UpdateLandingState();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        moving = Mathf.Abs(horizontal) > 0 ? 1 : 0;
        if (IsGrounded())
        {
            if (!isRunning && !Input.GetKey(KeyCode.Space)){
            stamina.Actions(2);
            }
        }
        Jump();
        AdjustSpeed();
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
        if (Input.GetKeyDown(KeyCode.F) && !GetComponent<Dash>().isDashing)
        {
            if (IsGrounded() || canCoyoteJump)
            {
                if (stamina.stamina < stamina.staminaJumpValue)
                {
                    return;
                }
                stamina.Actions(3);
                justLanded = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                isJumping = true;
                canCoyoteJump = false;
                StartCoroutine(ResetCoyoteJumpAfterDelay(coyoteTime));
            }
        }
    }

    private void AdjustSpeed()
    {
        if (Input.GetKey(KeyCode.Space) && moving != 0 && stamina.stamina > ( stamina.staminaRunValue)){
            isRunning = true;
            speed = speedVeloc;
            stamina.Actions(0);
        }
        else{
            isRunning = false;
            speed = origSpeed;
        }
         
        
    }

    private void UpdateAnimator()
    {
        anim.SetFloat("speed", speed);
        anim.SetFloat("isFacingRight", isFacingRight ? 1f : 0f);
        anim.SetInteger("isMoving", moving);
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
            if (hit.collider != null)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeAngle > 0 && slopeAngle < 90)
                {
                    float direction = hit.normal.x > 0 ? 1 : -1;
                    float rotationSpeed = 0.8f;
                    float targetRotation = direction * -slopeAngle;
                    float smoothRotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetRotation, Time.deltaTime * rotationSpeed);
                    transform.rotation = Quaternion.Euler(0, 0, smoothRotation);

                    speed = origSpeed * (1f + slopeAngle / 45f);
                }
                else
                {

                    float resetRotationSpeed = 3f;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * resetRotationSpeed);
                    speed = origSpeed;
                }
            }
        }
        else
        {
            float resetRotationSpeed = 3f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * resetRotationSpeed);
            speed = origSpeed;
        }
    }

    private void HandleGroundedState()
    {
        if (IsGrounded())
        {
            coyoteTimer = coyoteTime;
            canCoyoteJump = true;
            isJumping = false;
            hasLaunched = false;
        }
        else if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.fixedDeltaTime;
            canCoyoteJump = true;
        }
        else
        {
            canCoyoteJump = false;
        }
    }

    private void animatorHandler()
    {
        if (isJumping)
        {
            if (!hasLaunched)
            {
                // JumpParticle.Play();
                hasLaunched = true;
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private IEnumerator ResetCoyoteJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canCoyoteJump = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            GetComponentInChildren<SpriteRenderer>().flipX = !isFacingRight;
        }
    }

    private IEnumerator ResetJustLandedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        justLanded = false;
    }
}
