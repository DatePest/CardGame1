using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using System.Threading.Tasks;

public  abstract class Skill_UserTargetIfCkeck_AbilityTarget : Skill_NeedTargetBase
{
    
    [Header("IfCkeck=True > NeedAbilityTarget Seting ")]
    [SerializeField] protected UnitCheckBase IfCkeck;
    [SerializeField] protected bool IsCheckTurn =false;

    [SerializeField] protected bool EnforceTarget2 = false;
    [SerializeField] protected WaitAbilityTarget waitAbilityTarget;
    [Header("Base Seting")]
    [SerializeField] protected bool IsRandomTarget2 = false;
    [SerializeField] protected TargetRange targetRange_2;
    [SerializeField] protected MapShowTpye mapShow2;

    [Header("NeedUnit Seting")]
    [SerializeField]
    protected UnitSortSelectType SelectType2;
    [SerializeField]
    protected CheckSortTpye SortTpye2;
    [SerializeField]
    List<UnitCheckBase> unitChecks2;
    
    [Header("NeedMap Seting ")]
    [SerializeField] protected MapSelectType MapType;
    [SerializeField] protected  MapSelectCondition Mapcondition;
    [SerializeField] protected UnitTypeRange UnitTyperange2;

    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        var New_data = await Get_UserTarget1(data);
        bool b;
        if (IfCkeck != null)
            b = IfCkeck.UseCheck(New_data.UserTarget) == IsCheckTurn;
        else
            b = true;
        New_data.Use = b;

        if (EnforceTarget2 ==true|| b)
        {
          
            Debug.Log("請選擇使用目標2");

            switch (waitAbilityTarget)
            {
                case WaitAbilityTarget.Unit:
                    if (New_data.AbilityToTarget == null)
                    {
                        if (SelectType2 != UnitSortSelectType.Manual)
                            New_data.AbilityToTarget = RandomUnitTarget(New_data.CurrentUsePlayer, targetRange_2, SelectType2, SortTpye2, unitChecks2);
                        else
                        {
                            MouseShowType(New_data.CurrentUsePlayer, mapShow2);
                            while (New_data.AbilityToTarget == null)
                            {
                                New_data.AbilityToTarget = WaitUnit(New_data, targetRange_2, UnitTyperange2, unitChecks2);
                                await Task.Delay(300);
                            }
                        }
                    }
                        

                    break;
                case WaitAbilityTarget.Map:
                    if (New_data.MapSolt == null)
                    {
                        if (MapType != MapSelectType.Manual)
                            New_data.MapSolt = RandomMapTarget(New_data.CurrentUsePlayer, targetRange_2, MapType, Mapcondition);
                        else
                        {
                            MouseShowType(New_data.CurrentUsePlayer, mapShow2);
                            while (New_data.MapSolt == null)
                            {
                                New_data.MapSolt = WaitMapSolt(New_data, targetRange_2, Mapcondition);
                                await Task.Delay(300);
                            }
                        }
                    }
                       
                    break;
                case WaitAbilityTarget.Null:
                    break;
            }


        }
        return New_data;
    }
    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if (data.UserTarget == null)
            return false;
        switch (waitAbilityTarget)
        {
            case WaitAbilityTarget.Unit:
                if (data.AbilityToTarget == null)
                    return false;
                break;
            case WaitAbilityTarget.Map:
                if (data.MapSolt == null)
                    return false;
                break;
            case WaitAbilityTarget.Null:
                break;
        }
        return true;
    }
}



public enum WaitAbilityTarget:byte
{
    Null,Unit,Map
}





