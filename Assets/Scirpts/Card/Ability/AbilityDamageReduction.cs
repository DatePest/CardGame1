using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DamageReduction", menuName = "SO/AbilityBase/DamageReduction")]
public class AbilityDamageReduction : AbilityBase
{
    [Tooltip("實際傷害小於值則免疫該傷害")][SerializeField]
     int Value=0;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.Event_BeDamage += DamageReduction;
    }

    private void DamageReduction(SO_Unit BeUnit, SO_Unit arg2, int Damger)
    {
        if (Damger <= Value&& Damger>0)
            BeUnit.TakeHealHp(Damger);
    }


    public override void RemoveAbility(SO_Unit unit)
    {
        unit.Event_CauseDamage -= DamageReduction;
    }

    public override void Stack(bool IsAdd)
    {
        return;
    }
}
