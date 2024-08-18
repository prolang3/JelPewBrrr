using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerSlash : Projectile
{
    public override void Awake()
    {
        base.Awake();
       
    }

    public void Start()
    {
        print(Shooter);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, .5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != Shooter && !hitCollider.gameObject.CompareTag("Bullet") && hitCollider.GetComponent<HealthComponent>() != null)
            {
                hitCollider.gameObject.GetComponent<HealthComponent>().Health -= Damage;
            }
        }

    }

    // Update is called once per frame
    public override void Update()
    {
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            spriteRenderer.flipY = true;
        }
        else
        {            
            spriteRenderer.flipY = false;
        }
        print(transform.eulerAngles.z);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, 1f);
    }

}
