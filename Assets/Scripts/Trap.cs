using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Main main;
    public Player player;

    public float moveSpeed = 0.5f;
    public float maxMoveSpeed = 10f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
        player = main.player;
    }

    void Update()
    {
        if (!player)
        {
            return;
        }

        if (player.transform.position.y > transform.position.y + 16f)
        {
            Destroy(transform.parent.gameObject);
        }

        if (rb.bodyType != RigidbodyType2D.Static)
        {
            return;
        }

        if (player.transform.position.y >= transform.position.y && player.transform.position.y < transform.position.y + 4f)
        {
            if (moveSpeed > 0 && Input.GetKeyDown(KeyCode.Z))
            {
                Activate(true);
            }

            if (moveSpeed < 0 && Input.GetKeyDown(KeyCode.X))
            {
                Activate(true);
            }
        }

        if (player.transform.position.y > transform.position.y + 4f && player.IsGrounded())
        {
            Activate(false);
        }
    }

    void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            var v = rb.velocity;
            v.x = Mathf.Clamp(v.x + moveSpeed, -maxMoveSpeed, maxMoveSpeed);
            rb.velocity = v;
        }
    }

    public void Activate(bool isChaining)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        main.AddScore(isChaining);
    }
}
