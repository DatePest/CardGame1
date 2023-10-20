using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "UserTargetAndMap_IF", menuName = "SO/CardSkill/IF/UserTargetAndMap_IF")]
public sealed class Skill_UserTargetAndMap_If : Skill_UserTarget_NeedMapSolt
{
    [SerializeField] UnitCheckBase UnitCheck ;
    [SerializeField] List<Skill_UserTarget_NeedMapSolt> FalseCheck_UserTarget, TrunCheck_UserTarget;
    [SerializeField] List<Skill_NotTarget> FalseCheck_NotTarget, TrunCheck_NotTarget;
    Skill_UserTargetAndMap_If()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        if (UnitCheck.UseCheck(data.UserTarget))
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
