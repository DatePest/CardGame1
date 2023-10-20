using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeAddBuff_Event : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.BeAddBuff += IEvent;
    }

    protected abstract void IEvent(SO_Unit arg1, BuffBase arg2);

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.BeAddBuff -= IEvent;
    }
}
