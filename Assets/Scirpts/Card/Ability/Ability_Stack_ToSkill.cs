using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Stack_ToSkill", menuName = "SO/AbilityBase/Stack_ToSkill")]
public class Ability_Stack_ToSkill : AbilityBase
{
    [SerializeField] Event_Ability_UseSkill_NotTarge Event1;
    [SerializeField] Event_Ability_UseSkill_NeedTarge Event2;
    [SerializeField] int TargetTimes =1;
    SO_Unit MyUnit;
    int StackT;
    public override void AddUseAbility(SO_Unit unit)
    {
        StackT = 0;
        MyUnit = unit;
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        StackT = 0;
        MyUnit = null;
    }

    public override void Stack(bool IsAdd)
    {
        if (IsAdd)
            StackT++;
        else
            StackT--;

        if (StackT < 0)
            StackT = 0;
        Debug.Log("Stack" + StackT);
        if(StackT >= TargetTimes)
        {
            Event1.IEvent(MyUnit);
            Event2.IEvent(MyUnit);
        }

    }
}
