using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack_X", menuName = "SO/CardSkill/Attack/UserTarget_Attack_X")]
public sealed class Skill_Attack_X : Skill_UserTarget_NeedMapSolt
{
    Skill_Attack_X()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
        mapShow2 = MapShowTpye.X_Tpye;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Attack_X_Target AB = new(data);
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

        Round_Battle B = T as Round_Battle;
        B.BattleActionsAdd(AB);
    }
}
