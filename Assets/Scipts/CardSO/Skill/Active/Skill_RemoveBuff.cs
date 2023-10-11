using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "RemoveBuff", menuName = "SO/CardSkill/Add/RemoveBuff")]
public sealed class Skill_RemoveBuff : Skill_UserTarget
{
    [SerializeField] List<RoundTimeBuffSO> buffSOs;
    Skill_RemoveBuff()
    {
        skill_Rule = Enum_Skill_Rule.Null;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log(AbilityID + "___run");
        foreach (var a in buffSOs)
        {
            data.UserTarget.UnitData.RemoveBuff(a);
            //foreach (var A in data.UserTarget.UnitData.Buffs)
            //{
            //    if (A.BuffID == a.BuffID)
            //    {
                   
            //        break;
            //    }
            //}
        }
    }
}
