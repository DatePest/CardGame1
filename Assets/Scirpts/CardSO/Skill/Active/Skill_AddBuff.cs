using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "AddBuff", menuName = "SO/CardSkill/Add/AddBuff")]
public sealed class Skill_AddBuff : Skill_UserTarget
{
    [SerializeField] List<RoundTimeBuffSO> buffSOs;
    Skill_AddBuff()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log(AbilityID + "___run");
        foreach (var a in buffSOs)
        {
            data.UserTarget.UnitData.AddBuff(a);

        }
    }
}
