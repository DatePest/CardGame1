using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Hero005C_AddBuff", menuName = "SO/AbilityBase/Hero/Hero005C_skill")]
public sealed class Skill_AddBuff_Hero005B : Skill_UserTarget
{
    [SerializeField] List<RoundTimeBuffSO> buffSOs;
    [SerializeField] int Times = 3;
    Skill_AddBuff_Hero005B()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        int i = 0;
        foreach (var a in data.UserTarget.UnitData.Buffs)
        {
            if (a.buffTpye == BuffType.DeBuff)
            {
                i++;
                if (i >= Times)
                {
                    break;
                }

            }
        }

        for(int o =0; o<i; o++)
        {
            foreach (var a in buffSOs)
            {
                data.UserTarget.UnitData.AddBuff(a);

            }
        }

        
    }
}
