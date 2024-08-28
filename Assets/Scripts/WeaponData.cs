using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public bool isMelee = false;

    public GameObject Bullet;
    public Sprite Sprite;
    public float BaseDamage;
    public float UseDelay;

    public Vector2 PositionOffset;
    public Vector2 FireOffset;

    public AudioClip FireSound;
}
