using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public UnityEvent OnHit;

    public GameObject Bullet;

    public string Name = "Sample Text";
    public int BaseDamage = 1;
    public float UseDelay = 0.3f;
    public AudioClip FireSound;

    public void Fire(Vector3 originPosition, Vector3 targetPosition)
    {
        Vector3 diff = targetPosition - originPosition;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        quaternion rot = Quaternion.Euler(0f, 0f, rot_z - 90);
        Instantiate(Bullet, originPosition, rot);
        if (FireSound != null )
        {
            GameObject go = new GameObject();
            AudioSource FireSoundSource = go.AddComponent<AudioSource>();
            FireSoundSource.transform.localPosition = originPosition;
            FireSoundSource.clip = FireSound;
            FireSoundSource.Play();
            Destroy(go, 1);
        }
    }
}

