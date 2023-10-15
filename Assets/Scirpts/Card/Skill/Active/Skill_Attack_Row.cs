using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Attack_Row", menuName = "SO/CardSkill/Attack/UserTargetToTarget_Attack_Row")]
public sealed class Skill_Attack_Row : Skill_UserTarget_NeedMapSolt
{
    Skill_Attack_Row()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
        mapShow2 = MapShowTpye.Row_Tpye;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Attack_Row_Target AB = new(data);
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

        Round_Battle B = T as Round_Battle;
        B.BattleActionsAdd(AB);
    }
}
