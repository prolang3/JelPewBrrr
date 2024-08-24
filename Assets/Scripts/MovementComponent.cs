using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementComponent : MonoBehaviour
{
    public float Speed = 1f;
    public Vector2 Direction = Vector2.zero;
    public SpriteRenderer Sprite;
    public Rigidbody2D rb;

    public Vector2 Velocity = Vector2.zero;
    public bool isFacingRight = true;

    public Animator animator;

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
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Direction.normalized * Speed;
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(Direction.x) + Mathf.Abs(Direction.y));
        }
    }
}