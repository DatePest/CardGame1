using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IntValue_SkillDisCard", menuName = "SO/IntValue/SkillDisCard")]
public class IntValue_SkillDisCard : IntAbilityValue_SO
{
    public override int GetValue(AbilityNeedData data)
    {
        return data.CurrentUsePlayer.SkillDisCardCount;
    }
}
