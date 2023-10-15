using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_CauseDamage_Addbuff", menuName = "SO/AbilityBase/CauseDamage_Addbuff")]
public class Ability_Event_CauseDamage_Addbuff : BeDamage_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;
    
    public override void Stack(bool IsAdd)
    {
        return;
    }

    protected override void IEvent(SO_Unit BeDamageUnit, SO_Unit AtkUnit, int D)
    {
        if (D < 0) return;
        if (addTarget == AddTarget.AtkUnit)
            AddbuffBase.IEvent(AtkUnit);
        else
            AddbuffBase.IEvent(BeDamageUnit);
    }
}
