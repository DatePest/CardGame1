using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveStatsAddAbility", menuName = "SO/CardSkill/PassiveSkillBase/StatsAddAbility")]
public class StatsAddAbility : PassiveSkillBase
{
    [SerializeField]  AbilityBase Ability;
    public override void RemoveSkill()
    {
        if (Data != null)
        {
            Data.Value.UserTarget.UnitData.RemoveAbility(Ability);
            Data = null;
        }
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        data.UserTarget.UnitData.AddAbility(Ability);
        Data = data;
        return null;
    }

}
