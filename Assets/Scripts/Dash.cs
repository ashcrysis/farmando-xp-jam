using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private bool canDash = true;
    public float dashingPower = 10f;
    private float dashingTime = 0.35f;
    public float IFrames = 0.25f;
    public bool invincible = false;
    public float dashingCooldown = 1f;
    [SerializeField] private Transform dashCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    public bool isDashing;
    private bool isFacingRight;
    private float dir;
    private PlayerMovement player;
    public Stamina stamina;
    private CapsuleCollider2D playerCollider;
    private float colliderHeight;
    private float colliderY;
    public Transform wallBottomLeft;
    public Transform wallBottomRight;
    public Transform wallTopLeft;
    public Transform wallTopRight;
    public bool isWalled = false;
    public AudioSource dashAudio;
    private bool canPlayDashAudio = true;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        colliderHeight = playerCollider.size.y;
        colliderY = playerCollider.offset.y;
    }

    void Update()
    {
        isFacingRight = player.isFacingRight;
        dir = isFacingRight ? 1f : -1f;

        isWalled = checkIsWalled();
        if (isDashing)
        {
            GetComponent<DashTrail>().SetEnabled(true);
            playerCollider.size = new Vector2(playerCollider.size.x,1f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, (float)-0.49);
        }
        else
        {
            GetComponent<DashTrail>().SetEnabled(false);
            playerCollider.size = new Vector2(playerCollider.size.x,colliderHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, colliderY);
        }

        if (stamina.stamina < stamina.staminaDashValue  ){
            return;
        }
        if (!player.IsGrounded())
        {
            invincible = false;
        }
        if (Input.GetKeyDown(KeyCode.X) && ableDash() && canDash && !isWalled)
        {
            if (canPlayDashAudio)
            {
                dashAudio.Play();
                canPlayDashAudio = false;
            }
            StartCoroutine(DashCoroutine());
        }

    }

    private bool ableDash()
    {
        return Physics2D.OverlapCircle(dashCheck.position, 0.2f, groundLayer);
    }
    bool checkIsWalled(){
        return Physics2D.OverlapCircle(wallBottomLeft.position, 0.5f, groundLayer) || Physics2D.OverlapCircle(wallBottomRight.position, 0.5f, groundLayer) || Physics2D.OverlapCircle(wallTopRight.position, 0.5f, groundLayer) || Physics2D.OverlapCircle(wallTopLeft.position, 0.5f, groundLayer);
    }
    private IEnumerator DashCoroutine()
    {
        StartCoroutine(StartInvincibility());
        StartCoroutine(IncreaseJumpPower());
        canDash = false;
        stamina.Actions(1);
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * (dashingPower * dir), 0f);
        //rb.velocity = new Vector2(rb.velocity.x - dashingPower * (isFacingRight ? 1f : -1f) / 4, rb.velocity.y);
        yield return new WaitForSeconds(dashingTime);
        if (player.moving == 0)
        {
            rb.velocity = new Vector2(0f,rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(rb.velocity.x-(dashingPower * dir)/2,rb.velocity.y);
        }
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canPlayDashAudio = true;
        canDash = true;
    }
   private IEnumerator IncreaseJumpPower()
    {
        float originalJumpPower = player.jumpingPower; // Assume the PlayerMovement class has a jumpPower field
        player.jumpingPower += 0.5f;
        yield return new WaitForSeconds(0.4f);
        player.jumpingPower = originalJumpPower;
    }
 private IEnumerator StartInvincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(IFrames);
        invincible = false;
    }
}
