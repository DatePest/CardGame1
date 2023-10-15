using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_C049", menuName = "SO/CardSkill/Special/Special_C049")]
public sealed class Skill_AddBuff_Special_C049 : Skill_NotTarget_All
{
    [SerializeField] RoundTimeBuffSO buffSO;
    Skill_AddBuff_Special_C049()
    {
        skill_Rule = Enum_Skill_Rule.DirectDamage;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        var UL = CardGameManager.Instance.GetAllSelectRange(data, target);

        
        foreach (var a in UL)
        {
            if(a.UnitData.UnitTpye != UnitTpye.Barrier)
            foreach (var b in a.UnitData.Buffs)
            {
             
                if(b.BuffID == buffSO.BuffID)
                {
                    a.UnitData.TakeDamger(b.StackTimes, null, UnitHitTpye.Hit, AttackDamgerTpye.Enforce);
                    break;
                }
            }
        }
        
    }
}
