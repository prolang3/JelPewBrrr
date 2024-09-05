using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public string CurrentState = "Loading";

    public float MinRange = 0f;
    public float MaxRange = 10f;
    public float AttackBurstCooldown = .5f;
    public float AttackBurstDuration = 0.5f;
    public float RandomBurstCooldown = 1.2f;

    private float AttackBurstTimer = 0.1f;
    private float AttackBurstTimeLeft = 0f;

    [SerializeField]
    private GameObject target;
    private MovementComponent movementComponent;
    private WeaponHandler weaponHandler;
    private Weapon weapon;


    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        weaponHandler = GetComponent<WeaponHandler>();
        if (movementComponent == null)
        {
            movementComponent = gameObject.GetComponent<MovementComponent>();
        }
        if (weapon == null)
        {
            weapon = gameObject.GetComponent<Weapon>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        weapon.UpdateLocalPosition(transform.position, target.transform.position);

        if (target.transform.position.x < transform.position.x)
        {
            movementComponent.isFacingRight = false;
        }
        else if (target.transform.position.x > transform.position.x)
        {
            movementComponent.isFacingRight = true;
        }

        if (movementComponent.isFacingRight)
        {
            movementComponent.Sprite.flipX = false;
        }
        else
        {
            movementComponent.Sprite.flipX = true;
        }

        if (CurrentState != "Active")
        {
            return;
        }

        if (weaponHandler.Weapon.WeaponData.isMelee == false)
        {
            if ((target.transform.position - transform.position).magnitude <= MinRange)
            {
                movementComponent.Direction = -(target.transform.position - transform.position).normalized;
            }
            else
            
            if ((target.transform.position - transform.position).magnitude >= MaxRange)
            {
                movementComponent.Direction = (target.transform.position - transform.position).normalized;
            }
            else
            {
                //movementComponent.Direction /= 2;
                Attack();
            }
        }
        else
        {
            movementComponent.Direction = (target.transform.position - transform.position).normalized;
            Attack();
        }

        
    }

    void Attack()
    {
        if (AttackBurstTimer <= 0 && AttackBurstTimeLeft <= 0)
        {
            AttackBurstTimer = AttackBurstCooldown + Random.Range(0, RandomBurstCooldown);
            AttackBurstTimeLeft = AttackBurstDuration;
        }

        if (AttackBurstTimeLeft > 0)
        {
            weaponHandler.Attack(target.transform.position);
            AttackBurstTimeLeft -= Time.deltaTime;
        }
        if (AttackBurstTimer > 0)
        {
            AttackBurstTimer -= Time.deltaTime;
        }
    }
}
