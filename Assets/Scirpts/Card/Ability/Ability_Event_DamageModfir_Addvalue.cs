using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_DamageModfir_Addvalue", menuName = "SO/AbilityBase/Event_DamageModfir_Addvalue")]
public class Ability_Event_DamageModfir_Addvalue : DamageModifier_Event
{
    [SerializeField] int Addvalue;

    protected override int IEvent(int arg1, SO_Unit AtkUnit, SO_Unit behurt, AttackDamgerTpye arg4)
    {
        return arg1 + Addvalue;
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
