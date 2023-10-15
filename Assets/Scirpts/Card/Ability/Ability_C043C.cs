using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_C043C", menuName = "SO/AbilityBase/Ability_C043C")]
public class Ability_C043C : AttackStart_Event
{
    [SerializeField] SO_Unit Hero ;
    SO_Unit TargetUnit;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.Event_AtkStart += IEvent;
        TargetUnit = FindHero(unit);

    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.Event_AtkStart -= IEvent;
        TargetUnit = null;
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
    SO_Unit FindHero(SO_Unit U)
    {
        var Ul = CardGameManager.Instance.GetAllSelectRange(U.CurrentOwner, TargetRange.Own);

        foreach(var a in Ul)
        {
            if(a.UnitData.UintID == Hero.UintID)
            {
                return a.UnitData;
            }
        }
        return null;
    }

    protected override void IEvent(SO_Unit Atkunit, SO_Unit BeUnit)
    {
        if(TargetUnit != null)
        {
            BeUnit.TakeDamger(TargetUnit.LastAtk, TargetUnit, UnitHitTpye.Hit);
        }
    }
}
