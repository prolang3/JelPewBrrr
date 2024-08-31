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
    public int Pierce = 0;
    public bool doesflip = false;
    public bool doesFollowMouse = false;

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
        if (doesFollowMouse)
        {
            FollowMouse();
        }
    }

    public virtual void FollowMouse()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z
        Vector3 diff = mouseWorldPos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
        quaternion rot = Quaternion.Euler(0f, 0f, rotationOffset + rot_z);

        transform.rotation = rot;
    }

    public virtual bool CheckTeam(Collider2D collision)
    {
        if (collision == null || collision.gameObject == null || Shooter == null)
        {
            return false;
        }
        if (collision.gameObject.CompareTag(Shooter.tag) || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Room"))
        {
            return false;
        }
        if (collision.gameObject == Shooter)
        {
            return false;
        }
        return true;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CheckTeam(collision))
        {
            return;
        }
            if (collision.gameObject.GetComponent<HealthComponent>() == null)
        {
            Destroy(gameObject);
        }
        else
        {
            collision.gameObject.GetComponent<HealthComponent>().Health -= Damage;
            if (Pierce-- > 0)
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}
