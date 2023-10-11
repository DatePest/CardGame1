using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_C", menuName = "SO/CardSkill/Special/Special_C013")]
public sealed class Skill_AddBuff_Special_C013 : Skill_NotTarget_All
{
    [SerializeField] RoundTimeBuffSO TargetBuff,OtherBuff;
    [SerializeField] UnitCheckBase UnitCheck;
    Skill_AddBuff_Special_C013()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        int Times = 0;
        //var UList = CardGameManager.Instance.GetAllUnit();
        var UList = CardGameManager.Instance.GetAllSelectRange(data, target);
        Unit Target = null;
        foreach (var u in UList)
        {
            if(u )
            if (UnitCheck.UseCheck(u))
            {
                Target = u;
            }
            else
            {
                Times += u.UnitData.LastAtk;
                u.UnitData.AddBuff(OtherBuff);
            }
                
        }
        
        Debug.Log("DynamicSet = " + Times);
        var B = Target.UnitData.AddBuff(TargetBuff);
        if (B != null)
        {
            B.DynamicSet(Times);
        }
    }
}
