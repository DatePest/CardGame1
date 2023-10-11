using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Cover", menuName = "SO/AbilityBase/Ability_Cover")]
public class Ability_Cover : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.SetCover(true);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetCover(false);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
