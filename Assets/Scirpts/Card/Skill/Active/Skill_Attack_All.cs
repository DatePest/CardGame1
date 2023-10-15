using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack_All", menuName = "SO/CardSkill/Attack/UserTarget_Attack_All")]
public sealed class Skill_Attack_All : Skill_UserTarget
{
    Skill_Attack_All()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Attack_All_ToTarget AB = new(data);
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

        Round_Battle B = T as Round_Battle;
        B.BattleActionsAdd(AB);
    }
}
