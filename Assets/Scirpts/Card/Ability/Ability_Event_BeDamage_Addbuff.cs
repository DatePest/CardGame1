using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_BeDamage_Addbuff", menuName = "SO/AbilityBase/BeDamage_Addbuff")]
public class Ability_Event_BeDamage_Addbuff : BeDamage_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;
    protected override void IEvent(SO_Unit HurtUnit, SO_Unit AtkUnit, int arg3)
    {
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
