using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerEvent_UseEXSkill_UnitAddbuff", menuName = "SO/CardSkill/PlayerEvent/UseEXSkill_UnitAddbuff")]
public sealed class Skill_PlayerEvent_UseEXSkill_UnitAddbuff : Skill_NotTarget
{
    [SerializeField] BuffBase buff;
    PlayerOBJ player;
    int CurrentRound;
    [SerializeField] UnitCheck_UnitID unitID;
    public override async Task UseSkill(AbilityNeedData data)
    {
        var UList = CardGameManager.Instance.GetAllSelectRange(data, TargetRange.Own);
        foreach(var a in UList)
        {
            if (unitID.UseCheck(a))
            {
                a.UnitData.AddBuff(buff);
                break;
            }
        }
       
       
    }

}
