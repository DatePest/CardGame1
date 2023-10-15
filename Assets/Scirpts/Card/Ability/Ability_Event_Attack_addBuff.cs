using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Evene_Attack_addBuff", menuName = "SO/AbilityBase/Evene_Attack_addBuff")]
public class Ability_Event_Attack_addBuff : Attack_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;

    protected override void IEvent(SO_Unit AtkUnit, SO_Unit HurtUnit, int arg3)//‘¢¬ŠQšdˆÊ ,ó“ŠQšdˆÊ
    {
        if (arg3 < 1) return;
        if (addTarget == AddTarget.AtkUnit)
            AddbuffBase.IEvent(AtkUnit);
        else
            AddbuffBase.IEvent(HurtUnit);

    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
