using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Evene_AttackStart_addBuff", menuName = "SO/AbilityBase/Evene_AttackStart_addBuff")]
public class Ability_Event_AttackStart_addBuff : AttackStart_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;

    public override void Stack(bool IsAdd)
    {
        return;
    }

    protected override void IEvent(SO_Unit Atkunit, SO_Unit BeUnit)
    {
        if (addTarget == AddTarget.AtkUnit)
            AddbuffBase.IEvent(Atkunit);
        else
            AddbuffBase.IEvent(BeUnit);
    }
}
