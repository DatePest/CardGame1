using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Blessing", menuName = "SO/AbilityBase/Ability_Blessing")]
public class Ability_Blessing : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.SetIsBlessing(true);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetIsBlessing(false);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
