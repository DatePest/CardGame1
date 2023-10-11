using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack_Cross", menuName = "SO/CardSkill/Attack/UserTarget_Attack_Cross")]
public sealed class Skill_Attack_Cross : Skill_UserTarget_NeedMapSolt
{
    Skill_Attack_Cross()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
        mapShow2 = MapShowTpye.Cross_Tpye;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Attack_Cross_Target AB = new(data);
        Round_IState T = null;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);

        Round_Battle B = T as Round_Battle;
        B.BattleActionsAdd(AB);
    }
}
