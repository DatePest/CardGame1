using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "EnforceDamage", menuName = "SO/CardSkill/EnforceDamage")]
public sealed class Skill_EnforceDamage : Skill_UserTarget
{
    [Header("íºê⁄ë¢ê¨èùäQ ïsâ¬å∏ñ∆  ")]
    [SerializeField] protected int AbilityValue;
    Skill_EnforceDamage()
    {
        skill_Rule = Enum_Skill_Rule.DirectDamage;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        data.UserTarget.UnitData.TakeDamger(AbilityValue, null, UnitHitTpye.Hit, AttackDamgerTpye.Enforce);
    }
}
