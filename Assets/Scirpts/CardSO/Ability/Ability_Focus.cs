using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Focus", menuName = "SO/AbilityBase/Ability_Focus")]
public class Ability_Focus : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.SetFocus(true);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetFocus(false);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
