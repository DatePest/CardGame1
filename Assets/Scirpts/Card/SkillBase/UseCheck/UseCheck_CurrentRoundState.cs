using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "CurrentRoundState", menuName = "SO/UseCheck/CurrentRoundState")]
public class UseCheck_CurrentRoundState : SkillUseCheckBase
{
    [SerializeField] Round_State_Enum Round;
    public override async Task<CheckDate> UseCheck(AbilityNeedData data, CardSolt cardSolt, List<SO_CardCheckType> DisCard_Extra_SkillCheckList = null)
    {
        var checkDate = new CheckDate();
        var T = CardGameManager.Instance.GameTurnSystem.GetCurrentRoundState();
        if (T == Round)
        {
          
                checkDate.UseCheck = true;
        }
        return checkDate;
    }
}
