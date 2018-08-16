using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpPower = 15f;
    public LayerMask ground;
	
	Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer r;
    Animator anim; 

    public GameObject deadPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        r = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var v = rb.velocity;

        v.x = Input.GetAxis("Horizontal") * moveSpeed;

        var isGrounded = IsGrounded();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            v.y = jumpPower;
        }

        rb.velocity = v;

        if (rb.velocity.x > 0)
        {
            r.flipX = false;
        } 
		else if (rb.velocity.x < 0)
        {
            r.flipX = true;
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsGrounded", isGrounded);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Foe"))
        {
            Instantiate(deadPlayer, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public bool IsGrounded()
    {
        var hit = Physics2D.Raycast(
            new Vector2(col.bounds.min.x, col.bounds.min.y - 0.1f),
            Vector2.right,
            col.bounds.size.x,
            ground
        );

        return hit.collider != null;
    }
}
