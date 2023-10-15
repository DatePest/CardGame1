using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public abstract class Skill_UserTarget_NeedMapSolt : Skill_NeedTargetBase
{
    [SerializeField]
    protected MapSelectType SelectType2;
    [SerializeField]
    protected MapSelectCondition condition;
    [SerializeField]
    protected TargetRange targetRange_2;
    [SerializeField]
    protected MapShowTpye mapShow2;


    public async  override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        //data.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
        var New_data = await Get_UserTarget1(data);
        Debug.Log("請選擇使用目標格子");
        if (New_data.MapSolt == null)
        {
            if (SelectType2 != MapSelectType.Manual)
                New_data.MapSolt = RandomMapTarget(New_data.CurrentUsePlayer, targetRange_2, SelectType2, condition);
            else
            {
                MouseShowType(New_data.CurrentUsePlayer, mapShow2);
                while (New_data.MapSolt == null)
                {
                    New_data.MapSolt = WaitMapSolt(New_data, targetRange_2, condition);
                    await Task.Delay(300);
                }
            }
        }
            
        return New_data;
    }
    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if (data.UserTarget == null
            || data.MapSolt == null
            )
            return false;
        return true;
    }

}


