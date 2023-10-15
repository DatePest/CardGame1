using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_C043C", menuName = "SO/CardSkill/Special/Special_C043C")]
public sealed class Skill_AddBuff_Special_C043C : Skill_NotTarget_All
{
    [SerializeField] RoundTimeBuffSO buffSO;
    [SerializeField] SO_Unit Hero08;
    Skill_AddBuff_Special_C043C()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        var UL = CardGameManager.Instance.GetAllSelectRange(data, target);
        Unit T =null;
        foreach (var a in UL)
        {
            if(a.UnitData.UintID == Hero08.UintID)
            {
                T = a;
                break;
            }
        }
        UL.Remove(T);

       var Targrt = SetSortTpye(UL, UnitSortSelectType.Max, CheckSortTpye.Atk);
        Targrt.UnitData.AddBuff(buffSO);

    }
}
