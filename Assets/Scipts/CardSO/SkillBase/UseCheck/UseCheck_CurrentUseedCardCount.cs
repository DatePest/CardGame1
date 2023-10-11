using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "CurrentUseedCardCount", menuName = "SO/UseCheck/CurrentUseedCardCount")]
public class UseCheck_CurrentUseedCardCount : SkillUseCheckBase 
{
    [SerializeField] int Times;
    [SerializeField] IfCheck check;
    public override async Task<CheckDate> UseCheck(AbilityNeedData data, CardSolt cardSolt,List<SO_CardCheckType> DisCard_Extra_SkillCheckList =null)
    {
        var checkDate = new CheckDate();
        switch (check)
        {
            case IfCheck.Equal:
                if (data.CurrentUsePlayer.CurrentRoundUseedCardsConut == Times)
                {
                    checkDate.UseCheck = true;
                }
                break;
            case IfCheck.Greater:
                if (data.CurrentUsePlayer.CurrentRoundUseedCardsConut > Times)
                {
                    checkDate.UseCheck = true;
                }
                break;
            case IfCheck.Less:
                if (data.CurrentUsePlayer.CurrentRoundUseedCardsConut < Times)
                {
                    checkDate.UseCheck = true;
                }
                break;
        }
        return checkDate;
    }

     enum  IfCheck : byte
    {
        Equal,Greater, Less, 
    }


    public bool Check(PlayerOBJ P)
    {
        switch (check)
        {
            case IfCheck.Equal:
                if (P.CurrentRoundUseedCardsConut == Times)
                {
                    return true;
                }
                break;
            case IfCheck.Greater:
                if (P.CurrentRoundUseedCardsConut > Times)
                {
                    return true;
                }
                break;
            case IfCheck.Less:
                if (P.CurrentRoundUseedCardsConut < Times)
                {
                    return true;
                }
                break;
        }
        return false;
    }

}



