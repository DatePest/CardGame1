using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Skill_NeedTargetBase : ActiveSkillBase
{
    [SerializeField]
    protected UnitSortSelectType SelectType1;
    [SerializeField]
    protected CheckSortTpye SortTpye1;
    [SerializeField][Header("TargetRangeûäåè")]
    protected List<UnitCheckBase> unitChecks1;
    public TargetRange targetRange_1;

    [SerializeField] protected UnitTypeRange UnitTyperange;



    protected async Task<AbilityNeedData> Get_UserTarget1( AbilityNeedData data)
    {
        
        if (data.UserTarget == null)
        {
            if (SelectType1 != UnitSortSelectType.Manual)
                data.UserTarget = RandomUnitTarget(data.CurrentUsePlayer, targetRange_1, SelectType1, SortTpye1, unitChecks1);
            else
            {
                MouseShowType(data.CurrentUsePlayer, mapShow);
                while (data.UserTarget == null)
                {
                    data.UserTarget = WaitUnit(data, targetRange_1 , UnitTyperange, unitChecks1);
                    await Task.Delay(300);
                }
            }
        }
        return data;
    }

}

//public enum UnitTypeRange : byte
//{
//    // Normal,Hero, Summoner, Barrier
//    All, Hero, Barrier, Summoner
//}
[System.Serializable]
public class UnitTypeRange 
{
    public bool Hero = true, Summoner = false, Barrier = false;
}


