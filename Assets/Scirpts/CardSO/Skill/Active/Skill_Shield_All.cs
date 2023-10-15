using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Shield_All", menuName = "SO/CardSkill/Shield_All")]
public sealed class Skill_Shield_All : Skill_NotTarget_All
{
    [Header("Skill Need Use Value ")]
    [SerializeField] protected int AbilityValue;
    Skill_Shield_All()
    {
        skill_Rule = Enum_Skill_Rule.Heal;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {

        var UList = CardGameManager.Instance.GetAllSelectRange(data, target);
        foreach (var u in UList)
        {
            data.UserTarget.UnitData.SetUnitShield(AbilityValue);
        }

    }
}
