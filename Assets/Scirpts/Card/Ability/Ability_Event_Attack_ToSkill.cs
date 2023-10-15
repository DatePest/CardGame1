using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_Attack_ToSkill", menuName = "SO/AbilityBase/Evene_Attack_ToSkill")]
public class Ability_Event_Attack_ToSkill : Attack_Event
{
    [SerializeField] Event_Ability_UseSkill_NeedTarge Event;

    protected override void IEvent(SO_Unit AtkUnit, SO_Unit HurtUnit, int arg3)//‘¢¬ŠQšdˆÊ ,ó“ŠQšdˆÊ
    {
        if (addTarget == AddTarget.AtkUnit)
            Event.IEvent(AtkUnit);
        else
            Event.IEvent(HurtUnit);

    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
