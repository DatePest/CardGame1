using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Attack_Column", menuName = "SO/CardSkill/Attack/UserTargetToTarget_Attack_Column")]
public sealed class Skill_Attack_Column : Skill_UserTarget_NeedMapSolt
{
    Skill_Attack_Column()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
        mapShow2 = MapShowTpye.Column_Tpye;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Attack_Column_Target AB = new(data);
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref  T);

        Round_Battle B = T as Round_Battle;
        B.BattleActionsAdd(AB);
    }
}