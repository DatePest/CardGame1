using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ennemy_DirectDamage", menuName = "SO/CardSkill/DirectDamage")]
public sealed class Skill_DirectDamage : Skill_UserTarget
{   
    [Header("íºê⁄ë¢ê¨èùäQ â¬å∏ñ∆ ")]
    [SerializeField] protected int AbilityValue;
    [SerializeField] protected List<IntAbilityValue_SO> Int_so;
    Skill_DirectDamage()
    {
        skill_Rule = Enum_Skill_Rule.DirectDamage;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        data.AbilityID = AbilityID;
        int Dmg = AbilityValue;
        foreach (var a in Int_so)
        {
            Dmg += a.GetValue(data);
        }
        data.UserTarget.UnitData.TakeDamger(Dmg, null, UnitHitTpye.Hit, AttackDamgerTpye.Direct);
        //Debug.Log("Skill_DirectDamage");
    }
}

