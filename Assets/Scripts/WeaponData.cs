using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class WeaponData : MonoBehaviour
{
    public UnityEvent OnHit;

    public GameObject Bullet;
    public Sprite Sprite;

    public string Name = "Sample Text";
    public int BaseDamage = 1;
    public float UseDelay = 0.3f;
    public AudioClip FireSound;
    public Vector2 PositionOffset = Vector2.zero;
    public Vector2 FireOffset = Vector2.zero;


    public SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    public GameObject User;
    public GameObject Weapon;

    public void Init(GameObject user, GameObject weapon)
    {
        User = user;
        Weapon = weapon;
    }

    public void UpdateLocalPosition(Vector3 originPosition, Vector3 targetPosition)
    {
        Vector3 aimDirection = (targetPosition - originPosition).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Weapon.transform.eulerAngles = new Vector3(0, 0, angle);

        if (aimDirection.x > 0.01f)
        {
            isFacingRight = true;
            spriteRenderer.flipY = false;
            Weapon.transform.localPosition = PositionOffset;
        }
        if (aimDirection.x < 0.01f)
        {
            isFacingRight = false;
            spriteRenderer.flipY = true;
            Weapon.transform.localPosition = new Vector2(-PositionOffset.x, PositionOffset.y);
        }
    }

    public GameObject Fire(Vector3 targetPosition)
    {
        Vector3 originPosition = (Quaternion.Euler(0f, 0f, Weapon.transform.eulerAngles.z) * FireOffset) + new Vector3(Weapon.transform.position.x, Weapon.transform.position.y);
        if (!isFacingRight)
        {
            originPosition = (Quaternion.Euler(0f, 0f, Weapon.transform.eulerAngles.z) * new Vector2(FireOffset.x, -FireOffset.y)) + new Vector3(Weapon.transform.position.x, Weapon.transform.position.y);
        }

        Vector3 diff = targetPosition - originPosition;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
        quaternion rot = Quaternion.Euler(0f, 0f, rot_z);
        GameObject newBullet = Instantiate(Bullet, originPosition, rot);
        Projectile newBulletComponent = newBullet.GetComponent<Projectile>();
        newBullet.transform.rotation =  Quaternion.Euler(0,0, newBulletComponent.rotationOffset + rot_z);
        newBulletComponent.Shooter = User;
        newBulletComponent.Damage = BaseDamage;
        if (FireSound != null)
        {
            GameObject go = new GameObject();
            AudioSource FireSoundSource = go.AddComponent<AudioSource>();
            FireSoundSource.transform.localPosition = originPosition;
            FireSoundSource.clip = FireSound;
            FireSoundSource.Play();
            Destroy(go, 1);
        }

        return newBullet;
    }
}

