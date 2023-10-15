using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "UserTarget_Custom", menuName = "SO/CardSkill/IF/UserTarget_IfCkeck_Custom")]
public sealed class Skill_UserTarget_IfCkeck_Custom : Skill_UserTargetIfCkeck_AbilityTarget
{
    [SerializeField] List<ActiveSkillBase> FalseCheck_UserTarget, TrunCheck_UserTarget;
    [SerializeField] List<Skill_NotTarget> FalseCheck_NotTarget, TrunCheck_NotTarget;
    Skill_UserTarget_IfCkeck_Custom()
    {
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        if (data.Use)
        {
            if (TrunCheck_UserTarget.Count > 0)
                foreach (var a in TrunCheck_UserTarget)
                {
                    await a.UseSkill(data);
                }
            if (TrunCheck_NotTarget.Count > 0)
                foreach (var a in TrunCheck_NotTarget)
                {
                    await a.UseSkill(data);
                }
        }
        else
        {
            if (FalseCheck_UserTarget.Count > 0)
                foreach (var a in FalseCheck_UserTarget)
                {
                    await a.UseSkill(data);
                }
            if (FalseCheck_NotTarget.Count > 0)
                foreach (var a in FalseCheck_NotTarget)
                {
                    await a.UseSkill(data);
                }
        }
    }
}
