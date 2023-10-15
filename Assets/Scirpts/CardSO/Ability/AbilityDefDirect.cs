using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AbilityDefDirect", menuName = "SO/AbilityBase/AbilityDefDirect")]
public class AbilityDefDirect : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.DamageModifier += DefDirect;
    }

    private int DefDirect(int Damger, SO_Unit DamgerUnit, SO_Unit InjuriedUint, AttackDamgerTpye damgerTpy)
    {
        if(damgerTpy== AttackDamgerTpye.Direct)
            return 1;
        return Damger;
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.DamageModifier -= DefDirect;
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
