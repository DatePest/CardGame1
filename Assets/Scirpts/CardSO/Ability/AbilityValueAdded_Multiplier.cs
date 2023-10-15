using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ValueAdded_Multiplier", menuName = "SO/AbilityBase/ValueAdded_Multiplier")]
public class AbilityValueAdded_Multiplier : AbilityBase
{
    [SerializeField]
    AbilityMultiplierTpye AddedTarget;
    [SerializeField]
    float AddedTimes = 1;
    public override void AddUseAbility(SO_Unit unit)
    {
        AbilityBaseAPI.GetAbilityMultiplierSetFunc(unit, AddedTimes, AddedTarget);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        AbilityBaseAPI.GetAbilityMultiplierSetFunc(unit, AddedTimes, AddedTarget,true);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
