using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;


public sealed class Skill_Summoner : Skill_NeedMap
{
    Skill_Summoner()
    {
        skill_Rule = Enum_Skill_Rule.Summoner;
    }
    [SerializeField] SO_Unit unit;
    UnitTpye unitTpye = UnitTpye.Summoner;
    public override Task UseSkill(AbilityNeedData data)
    {

        if (!CardGameManager.Instance.IsSingleplayer && NetworkManager.Singleton.IsServer)
        {
            CardGame_Ctrl_Net.Instance.CreateUnit_ServerRpc(unit.UintID, unitTpye, (int)data.MapSolt.PlayerOwnerNumberID, data.MapSolt.MapSoltID);
        }
        else
            return null;
        
        return null;
    }
}
