using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_BeAtkStart_addBuff", menuName = "SO/AbilityBase/Event_BeAtkStart_addBuff")]
public class Ability_Event_BeAtkStart_addBuff : BeAtkStart_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;

    public override void Stack(bool IsAdd)
    {
        return;
    }

    protected override void IEvent(SO_Unit BeUnit, SO_Unit AtkUnit)
    {
        if (addTarget == AddTarget.AtkUnit)
            AddbuffBase.IEvent(AtkUnit);
        else
            AddbuffBase.IEvent(BeUnit);
    }
}
