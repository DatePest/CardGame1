using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_StopPassiveSkill", menuName = "SO/AbilityBase/Ability_StopPassiveSkill")]
public class Ability_StopPassiveSkill : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.PassiveSkillRemoveAll();
    }

    public override void Stack(bool IsAdd)
    {
        return;
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.PassiveSkillStartAll();
    }
}
