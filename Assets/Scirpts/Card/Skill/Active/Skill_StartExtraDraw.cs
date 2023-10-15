using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "StartExtraDraw", menuName = "SO/CardSkill/StartExtraDraw")]
public sealed class Skill_StartExtraDraw : Skill_NotTarget
{
    [SerializeField] int Times=1;

    Skill_StartExtraDraw()
    {
        skill_Rule = Enum_Skill_Rule.Draw;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        data.CurrentUsePlayer.SetRoundStartAddedCard(Times);
    }
}
