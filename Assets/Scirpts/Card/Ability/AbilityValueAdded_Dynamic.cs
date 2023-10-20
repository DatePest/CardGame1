using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ValueAdded_Dynamic", menuName = "SO/AbilityBase/ValueAdded_Dynamic")]
public class AbilityValueAdded_Dynamic: AbilityBase
{
    [SerializeField]
    AbilityTpye  AddedTarget;
    int AddedTimes = 0;
    public void SetAddedTimes(int i, SO_Unit U)
    {
        RemoveAbility(U);
        AddedTimes = i;
        AddUseAbility(U);
    }
    public override void AddUseAbility(SO_Unit unit)
    {
        AbilityBaseAPI.GetAbilitySetFunc(unit, AddedTimes, AddedTarget);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        AbilityBaseAPI.GetAbilitySetFunc(unit, -AddedTimes, AddedTarget);
        AddedTimes = 0;
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
