using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeDamage_Event : AbilityBase
{
    public AddTarget addTarget;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.Event_BeDamage += IEvent;
    }

    protected abstract void IEvent(SO_Unit BeDamageUnit, SO_Unit AtkUnit, int arg3);//,ó“ŠQšdˆÊ ,‘¢¬ŠQšdˆÊ 

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.Event_BeDamage -= IEvent;
    }
}
