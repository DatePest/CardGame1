using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public abstract class Skill_UserTarget : Skill_NeedTargetBase
{
    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        return await Get_UserTarget1(data);
    }
    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if (data.UserTarget == null
            )
            return false;
        return true;
    }
}


