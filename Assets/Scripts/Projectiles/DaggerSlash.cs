using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerSlash : Projectile
{
    private List<Collider2D> IgnoreList = new List<Collider2D>();

    public override void Awake()
    {
        base.Awake();
       
    }

    public void Start()
    {
        print(Shooter);

    }

    // Update is called once per frame
    public override void Update()
    {
        FollowMouse();
        Collider2D[]  hitColliders = Physics2D.OverlapCircleAll(transform.position, .6f);
        foreach (var hitCollider in hitColliders)
        {
            if (CheckTeam(hitCollider) && hitCollider.GetComponent<HealthComponent>() != null && !IgnoreList.Contains(hitCollider))
            {
                IgnoreList.Add(hitCollider);
                hitCollider.gameObject.GetComponent<HealthComponent>().Health -= Damage;
            }
        }

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            spriteRenderer.flipY = true;
        }
        else
        {            
            spriteRenderer.flipY = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, 1f);
    }

}
