using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Shield", menuName = "SO/CardSkill/Shield")]
public sealed class Skill_Shield : Skill_UserTarget
{
    [Header("Skill Need Use Value ")]
    [SerializeField] protected int AbilityValue;
    Skill_Shield()
    {
        skill_Rule = Enum_Skill_Rule.Heal;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        data.UserTarget.UnitData.SetUnitShield(AbilityValue);
        //Debug.Log("Shield" + AbilityValue);
    }
}
