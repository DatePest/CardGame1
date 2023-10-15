using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "AddAbility", menuName = "SO/CardSkill/Add/AddAbility")]
public sealed class Skill_AddAbility : Skill_UserTarget
{
    [SerializeField] List<AbilityBase> Abilitys;
    Skill_AddAbility()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log(AbilityID + "___run");
        foreach (var a in Abilitys)
        {
            data.UserTarget.UnitData.AddAbility(a);

        }
    }
}
