using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_BanEXUse", menuName = "SO/CardSkill/BanEXUse")]
public sealed class Skill_BanEXUse : Skill_NotTarget
{
    [SerializeField] bool IsWex;
    Skill_BanEXUse()
    {
        skill_Rule = Enum_Skill_Rule.Null;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        if (IsWex)
            data.CurrentUsePlayer.SetBanWEx(true);
        else
            data.CurrentUsePlayer.SetBanDEx(true);
    }
    
}
