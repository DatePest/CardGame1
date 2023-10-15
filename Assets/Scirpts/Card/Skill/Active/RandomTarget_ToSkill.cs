using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomTarget_ToSkill", menuName = "SO/CardSkill/RandomTarget_ToSkill")]
public sealed class Skill_RandomTarget_ToSkill : Skill_NotTarget
{
    [SerializeField] List<UnitCheckBase> UnitCheck ;
    [Header("Manual>>Random")]
    [SerializeField] UnitSortSelectType SelectType1 = UnitSortSelectType.Random;
    [SerializeField] CheckSortTpye SortTpye1;
    [SerializeField] TargetRange targetRange_1;
    [SerializeField] List<Skill_UserTarget> Skills;
    Skill_RandomTarget_ToSkill()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        if (SelectType1 == UnitSortSelectType.Manual) SelectType1 = UnitSortSelectType.Random;
        data.UserTarget = RandomUnitTarget(data.CurrentUsePlayer, targetRange_1, SelectType1, SortTpye1, UnitCheck);
        foreach (var a in Skills)
        {
            await a.UseSkill(data);
        }
    }
}
