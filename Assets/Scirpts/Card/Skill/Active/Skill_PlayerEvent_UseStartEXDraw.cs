using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerEvent_UseExCostDown ", menuName = "SO/CardSkill/PlayerEvent/UseExCostDown ")]
public sealed class Skill_PlayerEvent_UseExCostDown : Skill_NotTarget
{
    [SerializeField] int  Times = 1;
    [SerializeField] UseEXStartTpye eventTpye;


    public override async Task UseSkill(AbilityNeedData data)
    {
        if (eventTpye == UseEXStartTpye.SKill_CostDown)
            data.CurrentUsePlayer.SetExCostDown(Times);
        else
            data.CurrentUsePlayer.SetExCostDownKeepRound(Times);
    }
     enum UseEXStartTpye : byte
    {
        SKill_CostDown,
        SKill_CostDown_KeepRound,
    }
}
