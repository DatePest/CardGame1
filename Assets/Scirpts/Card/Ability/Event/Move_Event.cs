using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move_Event : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.MoveEnd += IEvent;
    }

    protected abstract void IEvent(SO_Unit obj);

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.MoveEnd -= IEvent;
    }
}
