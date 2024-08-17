using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementComponent : MonoBehaviour
{
    public float speed = 1f;
    public SpriteRenderer Sprite;
    public Rigidbody2D rb;

    public Vector2 Velocity = Vector2.zero;
    public bool isFacingRight = true;

    private Animator animator;

    public bool doesLookAtMouse = false;

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

    void Update()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z

        if (mouseWorldPos.x < transform.position.x)
        {
            isFacingRight = false;
        }
        else if (mouseWorldPos.x > transform.position.x)
        {
            isFacingRight = true;
        }

            if (isFacingRight)
        {
            Sprite.flipX = false;
        }
        else
        {
            Sprite.flipX = true;
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

        

        Velocity = direction.normalized * speed;
    }
}