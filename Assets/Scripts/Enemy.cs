using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public string CurrentState = "Loading";

    public float MinRange = 0f;
    public float MaxRange = 10f;
    public float AttackBurstCooldown = 1f;
    public float AttackBurstDuration = 0.5f;

    private float AttackBurstTimer = 0f;
    private float AttackBurstTimeLeft = 0f;

    private bool IsAttacking = false;

    [SerializeField]
    private GameObject target;
    private MovementComponent movementComponent;
    private WeaponHandler weaponHandler;
    private WeaponData weaponData;


    // Start is called before the first frame update
    void Start()
    {
        weaponHandler = GetComponent<WeaponHandler>();
        if (movementComponent == null)
        {
            movementComponent = gameObject.GetComponent<MovementComponent>();
        }
        if (weaponData == null)
        {
            weaponData = gameObject.GetComponent<WeaponData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
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

        if (AttackBurstTimer <= 0 && AttackBurstTimeLeft <= 0)
        {
            AttackBurstTimer = AttackBurstCooldown;
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

        movementComponent.Direction = (target.transform.position - transform.position).normalized;

        weaponData.UpdateLocalPosition(transform.position, target.transform.position);
        
    }
}
