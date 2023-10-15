using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Attack_Normal", menuName = "SO/CardSkill/Attack/UserTargetToTarget_Attack_Normal")]
public sealed class Skill_Attack_Normal : Skill_UserTargetToTarget
{
    Skill_Attack_Normal()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Attack_Normal_ToTarget AB = new(data);
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

        Round_Battle B = T as Round_Battle;
        B.BattleActionsAdd(AB);
    }
}
