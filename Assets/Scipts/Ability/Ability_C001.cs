using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_C001", menuName = "SO/AbilityBase/Ability_C001")]
public class Ability_C001 : AbilityBase
{
    [SerializeField] BuffBase buff;
    SO_Unit MyUnit;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.CurrentOwner.UseEXSkill += _Event;
        MyUnit = unit; 
    }

    private void _Event(GameObject obj)
    {
        MyUnit.AddBuff(buff);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.CurrentOwner.UseEXSkill -= _Event;
        MyUnit = null;
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
