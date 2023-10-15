using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "SO/CardSkill/Move")]
public sealed class Skill_Move : Skill_UserTarget_NeedMapSolt
{
    Skill_Move()
    {
        skill_Rule = Enum_Skill_Rule.Move;
        condition = MapSelectCondition.IsNotBarrier;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        //var TargetMap = data.MapSolt;
        MapSolt CurrentMap = data.UserTarget.GetComponentInParent<MapSolt>();
        //CurrentMap.Unitsolt.ReMoveUnit();
        var u = data.MapSolt.GetUnit(true);
        //if (u != null)
        //{
        //    data.MapSolt.Unitsolt.ReMoveUnit();
        //    CurrentMap.Net_unitSolt_SetUnitServerRpc(u);
        //}
        data.UserTarget.Move(data.MapSolt);
        //TargetMap.Net_unitSolt_SetUnitServerRpc(data.UserTarget);
        //TargetMap.Unitsolt.SetUnit(u);
       // await Task.Delay(3000);
        if (u != null)
        {
           // Debug.Log("A" + $"{ CurrentMap == data.UserTarget.CurrentMapSolt}");
           // Debug.Log("B" + $"{  data.MapSolt.GetUnit()==null}");
            u.Move(CurrentMap);

        }

    }
}
