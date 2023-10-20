using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "UserTarget_IF", menuName = "SO/CardSkill/IF/UserTarget_IF")]
public sealed class Skill_UserTarget_If : Skill_UserTarget
{
    [SerializeField] UnitCheckBase UnitCheck ;
    [SerializeField] List<Skill_UserTarget> FalseCheck_UserTarget, TrunCheck_UserTarget;
    [SerializeField] List<Skill_NotTarget> FalseCheck_NotTarget, TrunCheck_NotTarget;
    Skill_UserTarget_If()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        bool b = true;
        if (UnitCheck != null)
            b = UnitCheck.UseCheck(data.UserTarget);


        if (b)
        {
            foreach (var a in TrunCheck_UserTarget)
            {
                await a.UseSkill(data);
            }
            foreach (var a in TrunCheck_NotTarget)
            {
                await a.UseSkill(data);
            }
        }
        else
        {
            foreach (var a in FalseCheck_UserTarget)
            {
                await a.UseSkill(data);
            }
            foreach (var a in FalseCheck_NotTarget)
            {
                await a.UseSkill(data);
            }
        }
    }
}
