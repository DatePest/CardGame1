using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ValueAdded_Shield", menuName = "SO/AbilityBase/ValueAdded_Shield")]
public class AbilityValueAdded_Shield : AbilityBase
{
    public int Shield;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.SetUnitShield(Shield);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetUnitShield(-Shield);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
