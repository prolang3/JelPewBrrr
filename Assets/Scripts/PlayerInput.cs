using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    private MovementComponent movementComponent;
    private WeaponHandler weaponHandler;
    private WeaponData weaponData;

    void Start()
    {
        if (movementComponent == null)
        {
            movementComponent = GetComponent<MovementComponent>();
        }
        if (weaponHandler == null)
        {
            weaponHandler = GetComponent<WeaponHandler>();
        }
        if (weaponData == null)
        {
            weaponData = gameObject.GetComponent<WeaponData>();
        }
    }

    void Update()
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

        movementComponent.Direction = direction;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z

        if (mouseWorldPos.x < transform.position.x)
        {
            movementComponent.isFacingRight = false;
        }
        else if (mouseWorldPos.x > transform.position.x)
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

        weaponData.UpdateLocalPosition(gameObject.transform.position, mouseWorldPos);

        if (Input.GetMouseButton(0))
        {
            weaponHandler.Attack(mouseWorldPos);
        }
    }


}