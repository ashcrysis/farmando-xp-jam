using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private bool canDash = true;
    public float dashingPower = 10f;
    private float dashingTime = 0.3f;
    public float dashingCooldown = 1f;
    [SerializeField] private Transform dashCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    public bool isDashing;
    private bool isFacingRight;
    private float dir;
    private PlayerMovement player;
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        isFacingRight = player.isFacingRight;
        dir = isFacingRight ? 1f : -1f;
        if (Input.GetKeyDown(KeyCode.LeftControl) && ableDash() && canDash)
        {
            StartCoroutine(dash());
        }
        if (isDashing){
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        }
        else{
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
     private bool ableDash()
    {
        return Physics2D.OverlapCircle(dashCheck.position, 0.2f, groundLayer);

    }
       private IEnumerator dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * (dashingPower * dir), 0f);
        rb.velocity = new Vector2(rb.velocity.x-(dashingPower * (isFacingRight? 1f:-1f))/4,rb.velocity.y);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
