using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject WeaponObject;
    public Weapon Weapon;
    public float cooldown = 0f;
    public bool isMelee = false;

    void Start()
    {
        WeaponObject = new GameObject("gun");
        Weapon = GetComponent<Weapon>();

        WeaponObject.transform.parent = gameObject.transform;

        Weapon.spriteRenderer = WeaponObject.AddComponent<SpriteRenderer>();
        Weapon.spriteRenderer.sortingOrder = 1;

        Weapon.Init(gameObject, WeaponObject);
    }

    public void Attack(Vector2 mouseWorldPos)
    {
        if (cooldown <= 0)
        {
            cooldown = Weapon.UseDelay;
            GameObject bullet = Weapon.Fire(mouseWorldPos);
            if (isMelee)
            {
                bullet.transform.parent = transform;
            }
        }

        if (cooldown > 0)
        {
            if (Time.deltaTime > cooldown)
            {
                cooldown = 0f;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}
