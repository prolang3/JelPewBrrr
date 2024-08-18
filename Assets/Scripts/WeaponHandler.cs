using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject weapon;
    public WeaponData weaponData;
    public float cooldown = 0f;
    public bool isMelee = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        weapon = new GameObject("gun");
        weaponData = GetComponent<WeaponData>();
        weaponData.Init(gameObject, weapon);

        weapon.transform.parent = gameObject.transform;
        weapon.transform.localPosition = weaponData.PositionOffset;

        weaponData.spriteRenderer = weapon.AddComponent<SpriteRenderer>();
        weaponData.spriteRenderer.sprite = weaponData.Sprite;
        weaponData.spriteRenderer.sortingOrder = 1;
    }

    public void Attack(Vector2 mouseWorldPos)
    {
        if (cooldown <= 0)
        {
            cooldown = weaponData.UseDelay;
            GameObject bullet = weaponData.Fire(mouseWorldPos);
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
