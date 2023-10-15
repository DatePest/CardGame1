using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "SO/CardSkill/Heal")]
public sealed class Skill_Heal : Skill_UserTarget
{
    [Header("Skill Need Use Value ")]
    [SerializeField] protected int AbilityValue;
    Skill_Heal()
    {
        skill_Rule = Enum_Skill_Rule.Heal;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        data.UserTarget.UnitData.TakeHealHp(AbilityValue);
    }
}
