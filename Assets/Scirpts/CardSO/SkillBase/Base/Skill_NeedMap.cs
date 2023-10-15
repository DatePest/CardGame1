using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Skill_NeedMap : Skill_NeedMapBase
{
    
    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {

        if (data.MapSolt == null)
        {

            if (SelectType1 != MapSelectType.Manual)
                data.MapSolt = RandomMapTarget(data.CurrentUsePlayer, targetRange_1, SelectType1, condition);
            else
            {
                MouseShowType(data.CurrentUsePlayer, MapShow);
                while (data.MapSolt == null)
                {
                    data.MapSolt = WaitMapSolt(data, targetRange_1, condition);
                    await Task.Delay(300);
                }
            }
        }

        return data;
    }
    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if (data.MapSolt ==null
            )
            return false;
        return true;
    }
}
