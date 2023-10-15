using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawCard", menuName = "SO/CardSkill/DrawCard")]
public sealed class Skill_DrawCard : Skill_NotTarget
{
    [SerializeField] int Times=1;

    Skill_DrawCard()
    {
        skill_Rule = Enum_Skill_Rule.Draw;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
        {
            await Task.Delay(100);
        }
        data.CurrentUsePlayer.cardSpawnScript.SpawnSizeFromToTarget(Times, CardsPileEnum.deck, CardsPileEnum.hand);
        while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
        {
            await Task.Delay(500);
        }
    }
}
