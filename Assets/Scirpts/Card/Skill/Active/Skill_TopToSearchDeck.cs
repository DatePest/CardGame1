using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "TopToSearchDeck", menuName = "SO/CardSkill/TopToSearchDeck")]
public sealed class Skill_TopToSearchDeck: Skill_NotTarget
{
    [SerializeField] int Times = 1;

    [SerializeField] List<SO_CardCheckType> CheckList;

    Skill_TopToSearchDeck()
    {
        skill_Rule = Enum_Skill_Rule.Draw;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
        {
            await Task.Delay(100);
        }
        data.CurrentUsePlayer.cardSpawnScript.SpawnSizeFromToTarget(Times, CardsPileEnum.deck, CardsPileEnum.search);
        while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
        {
            await Task.Delay(400);
        }
        var search = data.CurrentUsePlayer.cardSpawnScript.FindCardsPile(CardsPileEnum.search);
        if (CheckList.Count > 0)
            foreach (var c in search.Cards)
            {
                while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
                {
                await Task.Delay(350);
                }
                bool Ckeck = false;
                foreach (var a in CheckList) 
                {
                    Ckeck = a.CheckCard(c);
                    if (Ckeck == true) break;
                }

                if(Ckeck)
                data.CurrentUsePlayer.cardSpawnScript.CardsPileChange(c, CardsPileEnum.search, CardsPileEnum.hand);
                else
                data.CurrentUsePlayer.cardSpawnScript.CardsPileChange(c, CardsPileEnum.search, CardsPileEnum.deck);
            }
        while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
        {
            await Task.Delay(100);
        }
    }


}
