using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementComponent : MonoBehaviour
{
    public float speed = 1f;
    public bool isFacingRight = true;
    public SpriteRenderer Sprite;
    public Rigidbody2D rb;

    public Vector2 Velocity = Vector2.zero;

    private Animator animator;

    void Start()
    {
        if (Sprite == null)
        {
            Sprite = GetComponent<SpriteRenderer>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        Move();
        rb.velocity = Velocity;
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Sqrt(Mathf.Pow(Velocity.x, 2) + Mathf.Pow(Velocity.y, 2)));
        }
    }

    void Move()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y += -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x += -1;
        }

        if ((isFacingRight && direction.x < 0) || (!isFacingRight && direction.x > 0))
        {
            Flip();
        }

        Velocity = direction.normalized * speed;
    }

    void Flip()
    {
        if (Sprite != null)
        {
            Sprite.flipX = !Sprite.flipX;
        }
        isFacingRight = !isFacingRight;

    }
}