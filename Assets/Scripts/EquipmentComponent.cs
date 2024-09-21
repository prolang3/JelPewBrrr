using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentComponent : MonoBehaviour
{ 
    public List<Item> Items = new();
    public delegate void OnAttackHandler();
    public event OnAttackHandler OnAttack;

    public Dictionary<Stat, float> GetStats()
    {
        Dictionary<Stat, float> statBonusList = new();

        foreach (Item item in Items)
        {
            foreach (KeyValuePair<Stat, float> statBonus in item.StatBonuses)
            {
                if (statBonusList.ContainsKey(statBonus.Key))
                {
                    statBonusList[statBonus.Key] += statBonus.Value;
                }
                else
                {
                    statBonusList.Add(statBonus.Key, statBonus.Value);
                }

                OnAttack?.Invoke();
            }
        }
        return statBonusList;
    }



}