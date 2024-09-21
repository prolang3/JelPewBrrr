using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    FlatDamageBonus,
    DamageBonus,
    DamageMultiplier,
    FlatMeleeDamageBonus,
    MeleeDamageBonus,
    MeleeDamageMultiplier,
    FlatMagicDamageBonus,
    MagicDamageBonus,
    MagicDamageMultiplier,
}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Dictionary<Stat, float> StatBonuses = new();

    public delegate void OnAttack();
    public delegate void OnDash();
}
