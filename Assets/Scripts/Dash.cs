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

        if (isDashing)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
            playerCollider.size = new Vector2(playerCollider.size.x,1f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, (float)-0.49);
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            playerCollider.size = new Vector2(playerCollider.size.x,colliderHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, colliderY);
        }
        if (invincible)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        if (stamina.stamina < stamina.staminaDashValue  ){
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && ableDash() && canDash )
        {
            StartCoroutine(DashCoroutine());
        }


    }

    private bool ableDash()
    {
        return Physics2D.OverlapCircle(dashCheck.position, 0.2f, groundLayer);
    }

    private IEnumerator DashCoroutine()
    {
        StartCoroutine(StartInvincibility());
        canDash = false;
        stamina.Actions(1);
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * (dashingPower * dir), 0f);
        rb.velocity = new Vector2(rb.velocity.x - (dashingPower * (isFacingRight ? 1f : -1f)) / 4, rb.velocity.y);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

 private IEnumerator StartInvincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(IFrames);
        invincible = false;
    }
}
