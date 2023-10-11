using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IntValue_SkillDisCard/2", menuName = "SO/IntValue/SkillDisCard/2")]
public class IntValue_SkillDisCard : IntAbilityValue_SO
{
    public override int GetValue(AbilityNeedData data)
    {
        return data.CurrentUsePlayer.SkillDisCardCount/2;
    }
}
