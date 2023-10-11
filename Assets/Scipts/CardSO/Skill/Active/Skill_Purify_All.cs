using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Purify_All", menuName = "SO/CardSkill/Purify_All")]

public sealed class Skill_Purify_All : Skill_NotTarget_All
{
    [SerializeField] PurifyTpye purifyTpye;
    Skill_Purify_All()
    {
        skill_Rule = Enum_Skill_Rule.Heal;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        var UList = CardGameManager.Instance.GetAllSelectRange(data, target);

        foreach (var u in UList)
        {
            u.UnitData.Purify(purifyTpye);
        }
    }
}
