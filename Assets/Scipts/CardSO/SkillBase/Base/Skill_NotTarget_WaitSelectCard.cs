using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public abstract class Skill_NotTarget_WaitSelectCard : Skill_NotTarget
{
    [SerializeField] protected CardsPileEnum From, From2= CardsPileEnum.Null, Target;
    [SerializeField] protected int Times = 1;
    [SerializeField] protected bool IsRandom=false;
    [SerializeField] protected List<SO_CardCheckType> CheckList ;

    [SerializeField] protected bool IsShuffle_Cards = true;

    public override async Task ReadySkillData(AbilityNeedData data)
    {
        var Updata = await UpSkillData(data);
        End(Updata);
    }
    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        while (data.CurrentUsePlayer.cardSpawnScript.SpawnEnd != true)
        {
            await Task.Delay(300);
        }
        var SearchEndCards = WaitSelectCard.GetFindCardsPile_To_SearchCheckCard(From, data, CheckList);
        if(From2 != CardsPileEnum.Null)
        {
            var SearchEndCards2 = WaitSelectCard.GetFindCardsPile_To_SearchCheckCard(From2, data, CheckList);
            SearchEndCards.AddRange(SearchEndCards2);
        }
       
        if (SearchEndCards.Count > 0)
        {
            if (IsRandom)
            {
                if (SearchEndCards.Count < Times)
                    data.SetSelectCardId(WaitSelectCard.RandomSelect(SearchEndCards, SearchEndCards.Count).ToArray());
                else
                    data.SetSelectCardId(WaitSelectCard.RandomSelect(SearchEndCards, Times).ToArray());
            }
            else
            {
                var CS = data.CurrentUsePlayer.CardSelectManager;
                if (SearchEndCards.Count < Times)
                    CS.DisplayStart_1(SearchEndCards, SearchEndCards.Count);
                else
                    CS.DisplayStart_1(SearchEndCards, Times);
                while (CS.SelectList.Count < Times)
                {
                    await Task.Delay(300);
                }
                data.SetSelectCardId(CS.SelectList.ToArray());
            }
        }
        return data;
    }
    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if ( data.SelectCardUids == null)
            return false;
        return true;
    }
    protected async void End(AbilityNeedData data)
    {
        UseNotify(data);
        await  UseSkill(data);

        if (From == CardsPileEnum.deck|| From2 == CardsPileEnum.deck && IsShuffle_Cards)
        {
            if (CardGameManager.Instance.IsSingleplayer)
                data.CurrentUsePlayer.cardSpawnScript.Shuffle_Cards(From);
            else
                CardGameManager.Instance.CardSpawnManager.ClientSynchronizServerRpc(data.CurrentUsePlayer.cardSpawnScript.OwnerID.Value, From,true);
        }
        while (data.CurrentUsePlayer.cardSpawnScript.InSynchronizeIng == true)
        {
            await Task.Delay(300);
        }
    }

    protected AbilityNeedData ReRandom(AbilityNeedData data)
    {
        var SearchEndCards = WaitSelectCard.GetFindCardsPile_To_SearchCheckCard(From, data, CheckList);
        if (SearchEndCards.Count > 1)
        {
            if (IsRandom)
            {
                if (SearchEndCards.Count < Times)
                    data.SetSelectCardId(WaitSelectCard.RandomSelect(SearchEndCards, SearchEndCards.Count).ToArray());
                else
                    data.SetSelectCardId(WaitSelectCard.RandomSelect(SearchEndCards, Times).ToArray());
            }
        }
        return data;
    }
    
}






