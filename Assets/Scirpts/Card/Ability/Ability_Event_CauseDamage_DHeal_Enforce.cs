using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_CauseDamage_DHeal", menuName = "SO/AbilityBase/Event_CauseDamage_DHeal")]
public class Ability_Event_CauseDamage_DHeal_Enforce : CauseDamage_Event
{
    [SerializeField] int Addvalue ;

    [SerializeField] float multiply =1;
    Ability_Event_CauseDamage_DHeal_Enforce()
    {
        addTarget = AddTarget.HurtUnit;
    }

    public override void Stack(bool IsAdd)
    {
        return;
    }

    protected override void IEvent(SO_Unit AtkUnit, SO_Unit BeDamageUnit, int D)
    {
        if (D < 0) return;
       if (addTarget == AddTarget.AtkUnit)
            AtkUnit.TakeHealHp(Addvalue + (int)(D * multiply));
        else
            BeDamageUnit.TakeHealHp(Addvalue + (int)(D * multiply));


    }
}
