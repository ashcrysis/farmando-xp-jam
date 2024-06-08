using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private bool canDash = true;
    public float dashingPower = 10f;
    private float dashingTime = 0.3f;
    private int IFrames = 10;
    private bool invincible = false;
    public float dashingCooldown = 1f;
    [SerializeField] private Transform dashCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    public bool isDashing;
    private bool isFacingRight;
    private float dir;
    private PlayerMovement player;
    private int invincibleEndFrame;
    public Stamina stamina;
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Debug.Log(stamina.stamina);
        isFacingRight = player.isFacingRight;
        dir = isFacingRight ? 1f : -1f;

        if (isDashing)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        if (stamina.stamina < stamina.staminaDashValue  ){
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && ableDash() && canDash)
        {
            StartCoroutine(DashCoroutine());
        }

        if (Time.frameCount >= invincibleEndFrame)
        {
            invincible = false;
        }

    }

    private bool ableDash()
    {
        return Physics2D.OverlapCircle(dashCheck.position, 0.2f, groundLayer);
    }

    private IEnumerator DashCoroutine()
    {
        StartInvincibility();
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

    private void StartInvincibility()
    {
        invincible = true;
        invincibleEndFrame = Time.frameCount + IFrames;
    }
}
