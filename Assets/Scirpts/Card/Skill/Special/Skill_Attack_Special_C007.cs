using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_C", menuName = "SO/CardSkill/Special/Special_C007")]
public sealed class Skill_Attack_Special_C007 : Skill_UserTargetToTarget
{
    Skill_Attack_Special_C007()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        if (data.UserTarget.UnitData.UnitName == "ãÅˆË")
        {
            Attack_Normal_ToTarget AB = new(data);
            Round_IState T = null;
            CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

            Round_Battle B = T as Round_Battle;
            B.BattleActionsAdd(AB);
        }
        else
        {
            Attack_All_ToTarget AB = new(data);
            Round_IState T = null;
            CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

            Round_Battle B = T as Round_Battle;
            B.BattleActionsAdd(AB);
        }
    }
}
