using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageModifier_Event : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.DamageModifier += IEvent;
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.DamageModifier -= IEvent;
    }
    //AtkUnit , behurt
    protected abstract int IEvent(int arg1, SO_Unit AtkUnit, SO_Unit behurt, AttackDamgerTpye arg4);
}
