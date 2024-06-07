using System.IO;
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
    private Dash DashScript;
    public float fallMultiplier = 1.5f;
    public ParticleSystem JumpParticle;
    public ParticleSystem LandParticle;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public int moving;
    Animator anim;
    private bool canCoyoteJump = false;
    private float coyoteTime = 0.15f; // Adjust as needed
    private float coyoteTimer = 0f;
    private bool isJumping = false;
    private bool hasLaunched = false;
    private bool justLanded = false;
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        origSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
        Flip();
        ApplyGravity();
        animatorHandler();
        UpdateLandingState();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        moving = Mathf.Abs(horizontal) > 0 ? 1 : 0;

        Jump();
        AdjustSpeed();
        //UpdateAnimator();
    }
private void UpdateLandingState()
{
    if (IsGrounded() && rb.velocity.y <= 0)
    {
        if (!justLanded)
        {
            //LandParticle.Play();
            justLanded = true;

        }
    }

}
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || canCoyoteJump)
            {
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
        speed = Input.GetButton("Fire3") ? speedVeloc : origSpeed;
    }

    private void UpdateAnimator()
    {
        anim.SetFloat("speed", speed);
        anim.SetFloat("isFacingRight", isFacingRight ? 1f : 0f);
        anim.SetInteger("isMoving", moving);


    }

    private void ApplyGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
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

    // Check ground slope and adjust rotation only when grounded
    if (IsGrounded())
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, groundLayer);
        if (hit.collider != null)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeAngle > 0 && slopeAngle < 90)
            {
                float direction = hit.normal.x > 0 ? 1 : -1;
                float rotationSpeed = 0.8f; // Adjust the rotation speed factor as needed
                float targetRotation = direction * -slopeAngle;
                float smoothRotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetRotation, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(0, 0, smoothRotation);
            }
            else
            {
                // Smoothly reset rotation if not on a slope
                float resetRotationSpeed = 3f; // Adjust the reset rotation speed factor as needed
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
        coyoteTimer = coyoteTime; // Reset the timer when grounded
        canCoyoteJump = true;
        isJumping = false;
        hasLaunched = false;

    }
    else if (coyoteTimer > 0)
    {
        coyoteTimer -= Time.fixedDeltaTime; // Decrease the timer
        canCoyoteJump = true;
    }
    else
    {
        canCoyoteJump = false; // Disable coyote jump when the timer reaches 0
    }
}
    private void animatorHandler()
    {
        if (isJumping)
        {

            if (!hasLaunched)
            {
                //JumpParticle.Play();
                hasLaunched = true;
            }
        }

    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.001f, groundLayer);
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
        }
    }
  // Add this coroutine to reset justLanded state after a delay
private IEnumerator ResetJustLandedAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    justLanded = false;
}
}
