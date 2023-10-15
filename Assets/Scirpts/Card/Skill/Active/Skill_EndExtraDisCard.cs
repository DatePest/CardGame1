using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "EndExtraDisCard", menuName = "SO/CardSkill/EndExtraDisCard")]
public sealed class Skill_EndExtraDisCard : Skill_NotTarget
{
    [SerializeField] int Times=1;

    Skill_EndExtraDisCard()
    {
        skill_Rule = Enum_Skill_Rule.Null;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        data.CurrentUsePlayer.SetEndDiscard(Times);
    }
}
