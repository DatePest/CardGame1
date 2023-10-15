using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Purify_TargerBuff", menuName = "SO/CardSkill/Purify_TargerBuff")]

public sealed class Skill_Purify_TargerBuff : Skill_UserTarget
{
    [SerializeField] PurifyTpye purifyTpye;
    [SerializeField] bool IsRandom = false , IsForceRemove =false;
    [SerializeField] BuffBase buff;
    Skill_Purify_TargerBuff()
    {
        skill_Rule = Enum_Skill_Rule.Heal;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        if(IsRandom == true)
        {
            buff = null;
            int T;
            while (buff != null)
            {
                T = Random.Range(0, data.UserTarget.UnitData.Buffs.Count -1);

                if (data.UserTarget.UnitData.Buffs[T].IsCanDispel != true &&  I_PurifyCheck.CheckType(data.UserTarget.UnitData.Buffs[T].buffTpye, purifyTpye))
                    buff = data.UserTarget.UnitData.Buffs[T];
            }
            
            
            
        }


        foreach(var a in data.UserTarget.UnitData.Buffs)
        {
            if(a.BuffID == buff.BuffID)
            {
                data.UserTarget.UnitData.Purify_Target(a, IsForceRemove);
            }
        }
    }
}