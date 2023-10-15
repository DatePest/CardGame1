using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack_Event : AbilityBase
{
    public AddTarget addTarget;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.Event_CauseDamage += IEvent;
    }

    protected abstract void IEvent(SO_Unit arg1, SO_Unit arg2, int arg3);//‘¢¬ŠQšdˆÊ ,ó“ŠQšdˆÊ

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.Event_CauseDamage -= IEvent;
    }
}
