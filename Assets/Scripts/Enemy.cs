using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
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

        movementComponent.Direction = (target.transform.position - transform.position).normalized;

        weaponData.UpdateLocalPosition(transform.position, target.transform.position);
        weaponHandler.Attack(target.transform.position);
    }
}
