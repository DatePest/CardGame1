using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStart_Event : AbilityBase
{
    public AddTarget addTarget;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.Event_AtkStart += IEvent;

        //Debug.Log( unit.MyUnit + "Sub_IEvent >> Sub" + AbilityId);
    }

    protected abstract void IEvent(SO_Unit Atkunit, SO_Unit BeUnit);//U?ŠJn Atkunit,BeUnit

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.Event_AtkStart -= IEvent;
    }
}
