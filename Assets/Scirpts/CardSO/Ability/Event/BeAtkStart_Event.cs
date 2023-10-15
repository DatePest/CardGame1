using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeAtkStart_Event : AbilityBase
{
    public AddTarget addTarget;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.Event_BeAtkStart += IEvent;
    }

    protected abstract void IEvent(SO_Unit BeUnit, SO_Unit AtkUnit);/// BeUnit,AtkUnit

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.Event_BeAtkStart -= IEvent;
    }
}
