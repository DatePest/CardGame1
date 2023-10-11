using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;

[CreateAssetMenu(fileName = "TopSearchReturn", menuName = "SO/CardSkill/TopSearchReturn")]
public sealed class TopSearchReturn : Skill_NotTarget_WaitSelectCard
{
    [Header("只能設定數量其他不能動")]
    [SerializeField] byte GotoTimesOnly;
    TopSearchReturn()
    {
        skill_Rule = Enum_Skill_Rule.Draw;
        From = CardsPileEnum.deck;
        IsRandom = false;
        IsShuffle_Cards = false;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        if(data.SelectCardUids == null|| data.SelectCardUids.Length <1)
        {
            return;
        }
        for (int i = 0; i < data.SelectCardUids.Length; i++)
        {
            Debug.Log("data" + data.SelectCardUids[i]);
        }

        var CardsPile = data.CurrentUsePlayer.cardSpawnScript.FindCardsPile(From).Cards;
        CardSolt[] Cards = new CardSolt[data.SelectCardUids.Length];
        for (int i=0;i< Cards.Length; i++)
        {
            Cards[i] = CardsPile[^(i + 1)].GetComponentInChildren<CardSolt>();
            Debug.Log("original"+Cards[i].CardSO.CardId);
        }
        SO_CardBase Tamp;
        for (int i = 0; i < Cards.Length; i++)
        {
            if(Cards[i].CardUid != data.SelectCardUids[i])
            {
                for (int U = i+1; U < Cards.Length; U++)
                {
                    if(Cards[U].CardUid == data.SelectCardUids[i])
                    {
                         Tamp = Cards[i].CardSO;
                        Cards[i].UpdateCardSO(Cards[U].CardSO);
                        Cards[U].UpdateCardSO(Tamp);
                    }
                }
            }
        }
        await Task.Delay(300);
        for (int i = 0; i < Cards.Length; i++)
        {
           
            Debug.Log("Setend" + Cards[i].CardSO.CardId);
        }
    }


    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        while (data.CurrentUsePlayer.cardSpawnScript.SpawnEnd != true)
        {
            await Task.Delay(300);
        }
        List<GameObject> SearchEndCards;
        var CardsPile = data.CurrentUsePlayer.cardSpawnScript.FindCardsPile(From).Cards;
        if (CardsPile.Count <= Times)
            SearchEndCards = new(CardsPile);
        else
        {
            SearchEndCards = new();
            for (int i = 0; i < Times; i++)
            {
                SearchEndCards.Add(CardsPile[^(i+1)]);//Cards[^1]
            }
        }
        if (SearchEndCards.Count > 0)
        {
            //if (IsRandom)
            //{
            //    if (SearchEndCards.Count < Times)
            //        data.SetSelectCardId(WaitSelectCard.RandomSelect(SearchEndCards, SearchEndCards.Count).ToArray());
            //    else
            //        data.SetSelectCardId(WaitSelectCard.RandomSelect(SearchEndCards, Times).ToArray());
            //}
            //else 
            {
                var CS = data.CurrentUsePlayer.CardSelectManager;
                if (SearchEndCards.Count < Times)
                    CS.DisplayStart_2(SearchEndCards, SearchEndCards.Count);
                else
                    CS.DisplayStart_2(SearchEndCards, Times);
                while (CS.SelectList.Count < Times)
                {
                    await Task.Delay(300);
                }
                data.SetSelectCardId(CS.SelectList.ToArray());
            }
        }
        return data;
    }

}
