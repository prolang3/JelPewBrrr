using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Effects
{
    public delegate void EffectDelegate();
    public Dictionary<string, EffectDelegate> OnAttackDict = new();
    
    

    void TestSkill()
    {

    }

    public List<EffectDelegate> GetEffects()
    {
        List<EffectDelegate> effectDelegates = new();

        EffectDelegate onAttackDelegate = TestSkill;
        effectDelegates.Add(onAttackDelegate);

        return effectDelegates;
    }
}



