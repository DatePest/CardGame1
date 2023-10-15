using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill_MapSelect_ToSkill", menuName = "SO/CardSkill/MapSelect_ToSkill")]
public sealed class Skill_MapSelect_ToSkill : Skill_NeedMap
{
    [SerializeField] List<Skill_UserTarget> Skills;
    Skill_MapSelect_ToSkill()
    {
        skill_Rule = Enum_Skill_Rule.Null;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log("UseSkill_Start");
        List<MapSolt> CurrentMaps;
        CurrentMaps = data.MapSolt.MyMapArea.Get_SwitchMaps(data.MapSolt, MapShow);
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
