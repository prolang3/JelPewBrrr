using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{

    public UnityEvent OnFire;

    public WeaponData WeaponData;

    public string Name = "Sample Text";

    public SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    public GameObject User;
    public GameObject WeaponObject;

    public void Init(GameObject user, GameObject weapon)
    {
        User = user;
        WeaponObject = weapon;
        spriteRenderer.sprite = WeaponData.Sprite;
    }

    public void UpdateLocalPosition(Vector3 originPosition, Vector3 targetPosition)
    {
        Vector3 aimDirection = (targetPosition - originPosition).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        WeaponObject.transform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 newOffset = WeaponData.PositionOffset;
        if (aimDirection.x > 0.01f)
        {
            isFacingRight = true;
            spriteRenderer.flipY = false;
            WeaponObject.transform.localPosition = Quaternion.Euler(0f, 0f, angle) * newOffset;
        }
        if (aimDirection.x < 0.01f)
        {
            isFacingRight = false;
            spriteRenderer.flipY = true;
            WeaponObject.transform.localPosition = Quaternion.Euler(0f, 0f, angle) * new Vector2(newOffset.x, -newOffset.y);
        }        
    }

    public GameObject Fire(Vector3 targetPosition)
    {
        Vector3 originPosition = (Quaternion.Euler(0f, 0f, WeaponObject.transform.eulerAngles.z) * WeaponData.FireOffset) + new Vector3(WeaponObject.transform.position.x, WeaponObject.transform.position.y);
        if (!isFacingRight)
        {
            originPosition = (Quaternion.Euler(0f, 0f, WeaponObject.transform.eulerAngles.z) * new Vector2(WeaponData.FireOffset.x, -WeaponData.FireOffset.y)) + new Vector3(WeaponObject.transform.position.x, WeaponObject.transform.position.y);
        }

        Vector3 diff = targetPosition - originPosition;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
        quaternion rot = Quaternion.Euler(0f, 0f, rot_z);
        GameObject newBullet = Instantiate(WeaponData.Bullet, originPosition, rot);
        Projectile newBulletComponent = newBullet.GetComponent<Projectile>();
        newBullet.transform.rotation =  Quaternion.Euler(0,0, newBulletComponent.rotationOffset + rot_z);
        newBulletComponent.Shooter = User;
        newBulletComponent.Damage = WeaponData.BaseDamage;
        if (WeaponData.FireSound != null)
        {
            GameObject go = new GameObject();
            AudioSource FireSoundSource = go.AddComponent<AudioSource>();
            FireSoundSource.transform.localPosition = originPosition;
            FireSoundSource.clip = WeaponData.FireSound;
            FireSoundSource.Play();
            Destroy(go, 1);
        }

        OnFire.Invoke();
        return newBullet;
    }
}