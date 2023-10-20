using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_BeDamage_DamgerAdd_Enforce", menuName = "SO/AbilityBase/Event_BeDamage_DamgerAdd")]
public class Ability_Event_BeDamage_DamgerAdd_Enforce : BeDamage_Event
{
    [SerializeField] int Addvalue ;

    [SerializeField] float multiply =1;

    protected override void IEvent(SO_Unit BeDamageUnit, SO_Unit AtkUnit, int arg3)
    {
        if(addTarget == AddTarget.HurtUnit)
            BeDamageUnit.TakeDamger(Addvalue + (int)(arg3* multiply), null, UnitHitTpye.Hit, AttackDamgerTpye.Enforce);
        else
            AtkUnit.TakeDamger(Addvalue + (int)(arg3 * multiply), null, UnitHitTpye.Hit, AttackDamgerTpye.Enforce);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
