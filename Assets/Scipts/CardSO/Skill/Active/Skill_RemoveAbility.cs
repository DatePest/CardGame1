using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "RemoveAbility", menuName = "SO/CardSkill/Add/RemoveAbility")]
public sealed class Skill_RemoveAbility : Skill_UserTarget
{
    [SerializeField] List<AbilityBase> Abilitys;
    Skill_RemoveAbility()
    {
        skill_Rule = Enum_Skill_Rule.Null;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log(AbilityID + "___run");
        foreach (var a in Abilitys)
        {
            data.UserTarget.UnitData.RemoveAbility(a);
            //foreach (var A in data.UserTarget.UnitData.Abilities)
            //{
            //    if(A.AbilityId == a.AbilityId)
            //    {
                    
            //        break;
            //    }
            //}
        }
    }
}
