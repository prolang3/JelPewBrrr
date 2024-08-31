using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject WeaponObject;
    public Weapon Weapon;
    public float cooldown = 0f;

    void Start()
    {
        WeaponObject = new GameObject("gun");
        Weapon = GetComponent<Weapon>();

        WeaponObject.transform.parent = gameObject.transform;

        Weapon.spriteRenderer = WeaponObject.AddComponent<SpriteRenderer>();
        Weapon.spriteRenderer.sortingOrder = 1;

        Weapon.Init(gameObject, WeaponObject);
    }

    private void Update()
    {
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

    public void Attack(Vector2 mouseWorldPos)
    {
        if (cooldown <= 0)
        {
            cooldown = Weapon.WeaponData.UseDelay;
            GameObject bullet = Weapon.Fire(mouseWorldPos);
            if (Weapon.WeaponData.isMelee == true)
            {
                bullet.transform.parent = transform;
            }
            cooldown = Weapon.WeaponData.UseDelay;
        }
    }
}
