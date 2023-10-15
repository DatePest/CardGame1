using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using System.Threading.Tasks;

public abstract class Skill_UserTargetToTarget : Skill_NeedTargetBase
{   [Header("Skill Goto GetAtk so... AbilityValue is Add Dmage")]
    [SerializeField] int AbilityValue = 0;
    [SerializeField] bool IsRandomTarget2 = false;
    public TargetRange targetRange_2;
    [SerializeField]
    protected MapShowTpye mapShow2;
    [SerializeField]
    protected UnitSortSelectType SelectType2;
    [SerializeField]
    protected CheckSortTpye SortTpye2;
    [SerializeField]
    protected List<UnitCheckBase> unitChecks2;


    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        //Debug.Log("請選擇使用目標1");
        var New_data = await Get_UserTarget1(data);
        if (New_data.AbilityToTarget == null)
        { //Debug.Log("請選擇使用目標2");
            if (SelectType2 != UnitSortSelectType.Manual)
                New_data.AbilityToTarget = RandomUnitTarget(New_data.CurrentUsePlayer, targetRange_2, SelectType2, SortTpye2, unitChecks2);
            else
            {
                MouseShowType(New_data.CurrentUsePlayer, mapShow2);
                while (New_data.AbilityToTarget == null)
                {
                    New_data.AbilityToTarget = WaitUnit(New_data, targetRange_2, UnitTyperange, unitChecks2);
                    await Task.Delay(300);
                }
            }
        }
        return New_data;
    }

    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if (    data.UserTarget == null 
            ||  data.AbilityToTarget ==null
            )
            return false;
        return true;
    }

}





