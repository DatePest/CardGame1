using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BePurified_Event : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.BePurified += IEvent;
    }

    protected abstract void IEvent(SO_Unit arg1, int arg2);

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.BePurified -= IEvent;
    }
}
