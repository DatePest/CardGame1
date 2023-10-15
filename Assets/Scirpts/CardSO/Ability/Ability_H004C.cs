using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_H004C", menuName = "SO/AbilityBase/Ability_H004C")]
public class Ability_H004C : AbilityBase
{
    [SerializeField] Event_Ability_UseSkill_NeedTarge Skill;
    [SerializeField] Buff_AnyTrun CheckBuff;

    SO_Unit MyHero;
    List<Unit> TargetUnit ;
    public override void AddUseAbility(SO_Unit unit)
    {
        TargetUnit = new();
        var Tamp = CardGameManager.Instance.GetAllSelectRange(unit.CurrentOwner, TargetRange.Enemy);
        foreach(var a in Tamp)
        {
            if (a.UnitData.UnitTpye != UnitTpye.Barrier)
            {
                TargetUnit.Add(a);
                a.UnitData.BeAddBuff += IEvent;
            }
        }
        MyHero = unit;
    }

    private void IEvent(SO_Unit arg1, BuffBase arg2)
    {
        if (CheckBuff.Check(arg2))
            Skill.IEvent(MyHero);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        foreach (var a in TargetUnit)
        {
            a.UnitData.BeAddBuff -= IEvent;
        }
        TargetUnit = null;

        MyHero = null;
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
