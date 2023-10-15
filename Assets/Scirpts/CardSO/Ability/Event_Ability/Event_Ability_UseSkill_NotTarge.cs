using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public  class Event_Ability_UseSkill_NotTarge : Event_AbilityBase
{
    public Skill_NotTarget Skill;
    public async void IEvent(SO_Unit Unit)//‘¢¬ŠQšdˆÊ ,ó“ŠQšdˆÊ
    {
        AbilityNeedData data = new();
        data.UserTarget = Unit.MyUnit;
        data.CurrentUsePlayer = Unit.CurrentOwner;

        await Skill.UseSkill(data);
    }
}