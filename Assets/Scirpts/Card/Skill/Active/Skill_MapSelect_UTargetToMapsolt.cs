using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill_MapSelect_UTargetToMapsolt", menuName = "SO/CardSkill/UTargetToMapsolt_ToSkill")]
public sealed class Skill_MapSelect_UTargetToMapsolt : Skill_UserTarget
{
    [SerializeField] List<Skill_UserTarget> Skills;
    [SerializeField] protected MapShowTpye Map_Show = MapShowTpye.Null;
    Skill_MapSelect_UTargetToMapsolt()
    {
        skill_Rule = Enum_Skill_Rule.Null;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log("UseSkill_Start");
        List<MapSolt> CurrentMaps;
        var map = data.UserTarget.CurrentMapSolt;
        CurrentMaps = map.MyMapArea.Get_SwitchMaps(map, Map_Show);
        Unit U;
        foreach (var a in CurrentMaps)
        {
            U = a.GetUnit();
            if (U != null && U.UnitData.UnitTpye == UnitTpye.Hero)
            {
                data.UserTarget = U;
                foreach(var S in Skills)
                {
                    //Debug.Log("UseSkill_Start_SKill");
                    await S.UseSkill(data);
                }
            }
        }
    }
}
