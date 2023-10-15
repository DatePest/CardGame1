using System.Threading.Tasks;
using UnityEngine;

public  abstract class Skill_UserTargetIfCkeck_2MapSelect : Skill_NeedTargetBase
{
    [Header("IfCkeck=True > NeedAbilityTarget Seting ")]
    [SerializeField] protected UnitCheckBase IfCkeck;
    [SerializeField] protected bool IsCheckTurn = false;


    [Header("Map1 Seting Trun")]
    [SerializeField]
    MapSelectType Map1Type;
    [SerializeField]
    MapSelectCondition Map1condition;
    [SerializeField] protected UnitTypeRange UnitTyperange1;
    [SerializeField] protected TargetRange MaptargetRange_1;
    [SerializeField] protected MapShowTpye mapShow1;
    [Header("Map2 Seting False")]
    [SerializeField]
    MapSelectType Map2Type;
    [SerializeField]
    MapSelectCondition Map2condition;
    [SerializeField] protected UnitTypeRange UnitTyperange2;
    [SerializeField] protected TargetRange MaptargetRange_2;
    [SerializeField] protected MapShowTpye mapShow2;



    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        var New_data = await Get_UserTarget1(data);
        bool b;
        if (IfCkeck != null)
            b = IfCkeck.UseCheck(New_data.UserTarget) == IsCheckTurn;
        else
            b = true;
        New_data.Use = b;

        if (b)
        {
            if (New_data.MapSolt == null)
            {
                if (Map1Type != MapSelectType.Manual)
                    New_data.MapSolt = RandomMapTarget(New_data.CurrentUsePlayer, MaptargetRange_1, Map1Type, Map1condition);
                else
                {
                    MouseShowType(New_data.CurrentUsePlayer, mapShow1);
                    while (New_data.MapSolt == null)
                    {
                        New_data.MapSolt = WaitMapSolt(New_data, MaptargetRange_1, Map1condition);
                        await Task.Delay(300);
                    }
                }
            }
        }
        else
        {
            if (New_data.MapSolt == null)
            {
                if (Map2Type != MapSelectType.Manual)
                    New_data.MapSolt = RandomMapTarget(New_data.CurrentUsePlayer, MaptargetRange_2, Map2Type, Map2condition);
                else
                {
                    MouseShowType(New_data.CurrentUsePlayer, mapShow2);
                    while (New_data.MapSolt == null)
                    {
                        New_data.MapSolt = WaitMapSolt(New_data, MaptargetRange_2, Map2condition);
                        await Task.Delay(300);
                    }
                }
            }
        }
        return New_data;
    }

    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if (data.UserTarget == null || data.MapSolt == null)
            return false;
        return true;
    }
}







