using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "GetExpansion_3", menuName = "SO/CardSkill/Attack/UserTargetToTarget_GetExpansion_3")]
public sealed class Skill_Attack_GetExpansion_3 : Skill_UserTarget_NeedMapSolt
{
    Skill_Attack_GetExpansion_3()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
        mapShow2 = MapShowTpye.GetExpansion_3;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Attack_GetExpansion_3_Target AB = new(data);
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

        Round_Battle B = T as Round_Battle;
        B.BattleActionsAdd(AB);
    }
}
