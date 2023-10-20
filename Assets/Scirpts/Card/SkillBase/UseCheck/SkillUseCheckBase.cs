using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SkillUseCheckBase : ScriptableObject
{
    public abstract  Task<CheckDate> UseCheck(AbilityNeedData data, CardSolt cardSolt, List<SO_CardCheckType> DisCard_Extra_SkillCheckList =null);
}
public struct CheckDate
{
    public CheckDate(bool Use = false, bool Ex =false)
    {
        UseCheck = Use;
        Extra = Ex;
    }

    public bool UseCheck;
    public bool Extra;
}
