using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "AnyUnitHaveBuff", menuName = "SO/UseCheck/AnyUnitHaveBuff")]
public class UseCheck_AnyUnitHaveBuff : SkillUseCheckBase
{
    [SerializeField] BuffBase Buff;
    [SerializeField] TargetRange target;
    public override async Task<CheckDate> UseCheck(AbilityNeedData data, CardSolt cardSolt, List<SO_CardCheckType> DisCard_Extra_SkillCheckList = null)
    {
        var checkDate = new CheckDate();
        var UL = CardGame_Ctrl_Net.Instance.GetAllSelectRange(data, target);
        foreach(var a in UL)
        {
            Debug.Log(a.UnitData.UID);
            foreach (var b in a.UnitData.Buffs)
            {
                
                if(b.BuffID == Buff.BuffID)
                {
                    Debug.Log(b.BuffID);
                    checkDate.UseCheck = true;
                    return checkDate;
                }
            }
        }
        return checkDate;
    }
}
