using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_StopGetBuff", menuName = "SO/AbilityBase/Ability_StopGetBuff")]
public class Ability_StopGetBuff : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.SetIsBanAddbuff(true);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetIsBanAddbuff(false);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
