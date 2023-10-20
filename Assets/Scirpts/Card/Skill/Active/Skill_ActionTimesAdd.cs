using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionTimesAdd", menuName = "SO/CardSkill/ActionTimesAdd")]
public sealed class Skill_ActionTimesAdd : Skill_NotTarget
{
    [SerializeField] int Times;
    Skill_ActionTimesAdd()
    {
        skill_Rule = Enum_Skill_Rule.ActionTimes;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        data.CurrentUsePlayer.AddActionTimes(Times);
        //Debug.Log("Current = " + data.CurrentUsePlayer.ActionTimes);
    }

}
