using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_C017", menuName = "SO/CardSkill/Special/Special_C017")]
public sealed class Skill_AddBuff_Special_C017 : Skill_UserTarget
{
    [SerializeField] List<RoundTimeBuffSO> buffSOs;
    Skill_AddBuff_Special_C017()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        List<BuffBase> T = new(data.UserTarget.UnitData.Buffs);
        foreach (var a in T)
        {
            data.UserTarget.UnitData.RemoveBuff(a);
        }
        foreach (var a in buffSOs)
        {
            data.AbilityToTarget.UnitData.AddBuff(a);
        }
    }
}
