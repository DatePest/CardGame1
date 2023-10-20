using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_C", menuName = "SO/CardSkill/Special/Card_AllTarget")]
public  class Card_SpecialBase_AllTarget : Skill_NotTarget_All
{
    [SerializeField]
    [Header("‘Š“¯Dataá¢“®‹Z”\")]
    List<Skill_NeedTargetBase> Skills;
    [SerializeField]
    [Header("ÌŠü”ä›”ˆ×^")]
    List<Skill_NeedTargetBase> ExtraSkills;

    [SerializeField]
    [Header("šdˆÊâ¿‘IŠŒ")]
    List<UnitCheckBase> unitChecks;

    protected async Task RunAbilities(AbilityNeedData data)
    {
        foreach (var a in Skills)
        {
            await a.UseSkill(data);
        }
    }
    protected async Task RunExtraAbilities(AbilityNeedData data)
    {
        if (!data.Use) return;
        foreach (var a in ExtraSkills)
        {
            await a.UseSkill(data);
        }
        
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        var UList = CardGame_Ctrl_Net.Instance.GetAllSelectRange(data, target);
        foreach (var u in UList)
        {
            bool B = true;
            foreach (var b in unitChecks)
            {
                if (!b.UseCheck(u))
                {
                     B = false;
                    break;
                }
                    
            }
            if (B != true) continue;
            if (data.UserTarget == null || data.UserTarget != u)
                data.UserTarget = u;
            if (Skills.Count != 0)
                await RunAbilities(data);

            if (ExtraSkills.Count != 0 && data.Use)
                await RunExtraAbilities(data);
        }
    }
}
