using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CauseDamage_Event : AbilityBase
{
    public AddTarget addTarget;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.Event_CauseDamage += IEvent;
    }

    protected abstract void IEvent(SO_Unit AtkUnit, SO_Unit BeDamageUnit, int D);//U?šdˆÊ ‘¢¬ŠQšdˆÊ ,ó“ŠQšdˆÊ(This)

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.Event_CauseDamage -= IEvent;
    }
}
