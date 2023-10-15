using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Special_C", menuName = "SO/CardSkill/Special/Card_NeedTarget")]
public   class Card_SpecialBase_NeedTarget : Skill_UserTarget
{
    [SerializeField]
    [Header("‘Š“¯Dataá¢“®‹Z”\")]
    List<Skill_NeedTargetBase> Abilities;
    [SerializeField]
    [Header("EXuseˆ×^")]
    List<Skill_NeedTargetBase> ExtraAbilities;

    protected async Task RunAbilities(AbilityNeedData data)
    {
        foreach(var a in Abilities)
        {
            await a.UseSkill(data);
        }
    }
    protected async Task RunExtraAbilities(AbilityNeedData data)
    {
        foreach (var a in ExtraAbilities)
        {
            await a.UseSkill(data);
        }
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        await RunAbilities(data);
        if (data.Use)
            await RunExtraAbilities(data);
    }
}


