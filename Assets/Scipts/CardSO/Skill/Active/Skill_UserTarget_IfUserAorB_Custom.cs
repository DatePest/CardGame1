using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "UserTarget_Custom", menuName = "SO/CardSkill/IF/UserTarget_IfUserAorB_Custom")]
public sealed class Skill_UserTarget_IfUserAorB_Custom : Skill_UserTarget
{
    [Header("Use A_SelectAorB//data.AbilityValue4 1==turn ")]
    [SerializeField] List<ActiveSkillBase> FalseCheck_UserTarget, TrunCheck_UserTarget;

   
    Skill_UserTarget_IfUserAorB_Custom()
    {
      
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        if (data.AbilityValue4 == 1)
        {
            foreach (var a in TrunCheck_UserTarget)
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
        }
    }
    A_SelectAorB A_SelectAorB = new();
    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        var NewData = await A_SelectAorB.UserSelectAorB(data);

        return await base.UpSkillData(NewData);
    }
}
