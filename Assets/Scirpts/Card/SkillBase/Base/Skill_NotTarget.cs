using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public abstract class Skill_NotTarget : ActiveSkillBase
{
    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        return data;
    }
    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        return true;
    }

}


