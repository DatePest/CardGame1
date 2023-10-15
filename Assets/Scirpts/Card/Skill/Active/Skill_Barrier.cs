using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrier", menuName = "SO/CardSkill/Barrier")]
public sealed class Skill_Barrier : Skill_NeedMap
{
    Skill_Barrier()
    {
        skill_Rule = Enum_Skill_Rule.Barrier;
    }
    [SerializeField] SO_Unit unit;
    UnitTpye unitTpye = UnitTpye.Barrier;
    public async override Task UseSkill(AbilityNeedData data)
    {
        if (!CardGameManager.Instance.IsSingleplayer && NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
        {
            CardGameManager.Instance.CreateUnit_ServerRpc(unit.UintID, unitTpye, (int)data.MapSolt.PlayerOwnerNumberID, data.MapSolt.MapSoltID);
        }
        await Task.Delay(300);
    }
}
