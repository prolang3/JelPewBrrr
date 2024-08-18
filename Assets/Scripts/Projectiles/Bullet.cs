using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Shooter;
    public float speed = 1f;
    public float lifeTime = 5f;
    public float Damage = 1f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = gameObject.transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
