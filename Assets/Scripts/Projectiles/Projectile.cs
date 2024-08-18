using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Shooter;
    public float speed = 1f;
    public float lifeTime = 5f;
    public float Damage = 1f;
    public Rigidbody2D rb;
    public float rotationOffset = 0;
    public bool doesflip = false;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        rb.velocity = gameObject.transform.up * speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject != Shooter && !collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.GetComponent<HealthComponent>() != null)
            {
                collision.gameObject.GetComponent<HealthComponent>().Health -= Damage;
            }
            Destroy(gameObject);
        }
    }
}
