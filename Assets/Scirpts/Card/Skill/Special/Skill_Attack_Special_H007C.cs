using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_H007C", menuName = "SO/CardSkill/Special/Special_H007C")]
public sealed class Skill_Attack_Special_H007C : Skill_UserTarget
{
    Skill_Attack_Special_H007C()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log("H007C ");
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);
        Round_Battle B = T as Round_Battle;
        int i = 0;
        foreach(var a in B.Battle_Actions)
        {
            if(a.SkillData.UserTarget == data.UserTarget)
            {
                i++;
            }
        }
        if (i >= 3)
        {
            Attack_All_ToTarget AB = new(data);
            B.BattleActionsAdd(AB);
            Debug.Log("H007C is add");
        }
      
    }
}
