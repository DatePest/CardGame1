using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_H012C", menuName = "SO/AbilityBase/Ability_H012C")]
public class Ability_H012C : AbilityBase
{
    int StackT;
    public override void AddUseAbility(SO_Unit unit)
    {
        StackT = 0;
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetUnitShield(1 + StackT);
    }

   
    public override void Stack(bool IsAdd)
    {
        if (IsAdd)
            StackT++;
        else
            StackT--;
        if (StackT < 0)
            StackT = 0;
    }
}
