using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "CurrentInthe", menuName = "SO/UseCheck/CurrentInthe")]
public class UseCheck_CurrentInthe : SkillUseCheckBase
{
    [SerializeField] CardSoltInThe Current, Before;
    public override async Task<CheckDate> UseCheck(AbilityNeedData data, CardSolt cardSolt, List<SO_CardCheckType> DisCard_Extra_SkillCheckList = null)
    {
        var checkDate = new CheckDate();
        if (cardSolt.CardCurrentInThe == Current )
        {
            if(cardSolt.CardBeforeInThe == Before|| cardSolt.CardBeforeInThe == CardSoltInThe.Null)
                checkDate.UseCheck = true;
        }
        return checkDate;
    }
}
