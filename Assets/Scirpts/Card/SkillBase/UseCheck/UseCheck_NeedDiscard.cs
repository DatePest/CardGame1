using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "NeedDiscard", menuName = "SO/UseCheck/NeedDiscard")]
public class UseCheck_NeedDiscard: SkillUseCheckBase
{
    [SerializeField] int Times=1;
    [SerializeField] protected CardsPileEnum From, Target;
    [SerializeField] protected bool IsRandom = false;
    [SerializeField] List<SO_CardCheckType> CheckList;
    [SerializeField] bool IsUseDisCardAbilities = true;
    [SerializeField] protected bool IsShuffle_Cards = true;

    public async override Task<CheckDate> UseCheck(AbilityNeedData data, CardSolt cardSolt, List<SO_CardCheckType> DisCard_Extra_SkillCheckList =null)
    {
        var checkDate = new CheckDate();
        if (data.CurrentUsePlayer.cardSpawnScript.FindCardsPile(From).Cards.Count >= Times)
        {
            var SearchEndCards =  WaitSelectCard.GetFindCardsPile_To_SearchCheckCard(From, data, CheckList);
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
                    var CS = CardGame_PlayerUIManager.Instance.Get_CardSelectManager();
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
                if (CardGameManager.Instance.IsSingleplayer != true)
                    CardGameManager.Instance.GameNotifyAction_Net.UseCheckDisCardServerRpc(new Net_AbilityNeedData(data), From, Target);
                var Cards = await data.CurrentUsePlayer.cardSpawnScript.FindCardGoto(data.SelectCardUids, From, Target);

                /////////////////////////////////////////////////////////////////
                while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
                {
                    await Task.Delay(300);
                }
                if (From == CardsPileEnum.deck && IsShuffle_Cards) /// 重新同步牌組
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
                while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
                {
                    await Task.Delay(300);
                }

                ///判斷是否執行額外
                if (DisCard_Extra_SkillCheckList!=null)
                {

                    foreach(var c in Cards)
                    {
                        foreach (var a in DisCard_Extra_SkillCheckList)
                        {
                            var T =a.CheckCard(c);
                            if (T == true)
                            {
                                checkDate.Extra = true;
                                break;
                            }
                        }
                        if (checkDate.Extra == true)
                            break;
                    }
                }
                if (From == CardsPileEnum.hand && Target == CardsPileEnum.disdeck)
                {
                    data.CurrentUsePlayer.UseSkillDisCardEvent(Cards.Count);
                }
                if (IsUseDisCardAbilities) //開始執行有效棄卡
                {
                    await WaitSelectCard.UseDisCardAbilities(Cards);
                }
                checkDate.UseCheck = true;
                return checkDate;
            } 
        }
        return checkDate;
    }
    public void SetTimes(int i)
    {
        Times = i;
    }
}
public static class WaitSelectCard
{
    public static async Task UseDisCardAbilities(List<GameObject> GList)
    {
        foreach (var a in GList)
        {
            var b = a.GetComponentInChildren<CardSolt>();
            b.UseDisCardAbilities();
            while (b.DisCardUseingLook != false)
            {
                await Task.Delay(500);
            }
        }
        return ;
    }



       public static List<GameObject> GetFindCardsPile_To_SearchCheckCard(CardsPileEnum From, AbilityNeedData data, List<SO_CardCheckType> CheckList)
    {
        var T = GetFindCardsPile(From, data);
        return SearchCheckCard(T, CheckList);
    }

    public static List<GameObject> GetFindCardsPile(CardsPileEnum From, AbilityNeedData data)
    {
        var List = data.CurrentUsePlayer.cardSpawnScript.FindCardsPile(From).Cards;
        GameObject c = null;
        // Remove Useing Card
        foreach (var a in List)
        {
            var b = a.GetComponentInChildren<CardSolt>();
            if (b.CardUseingLook == true)
            {
                c = a;
                break;
            }
        }

        if (c != null)
            List.Remove(c);

        return List;
    }

    public static List<GameObject> SearchCheckCard(List<GameObject> GList, List<SO_CardCheckType> CheckList)
    {
        if (CheckList.Count < 1) return GList;
        List<GameObject> NewList = new(GList);
        //Debug.Log("S" + NewList.Count);
        foreach (var a in CheckList)
        {
            NewList = a.SearchCheckCard(NewList);

            //Debug.Log("fore"+NewList.Count);
        }

        //Debug.Log("end"+NewList.Count);
        return NewList;
    }
    public static List<int> RandomSelect(List<GameObject> GList, int Times)
    {
        List<int> NewList = new();
        for (int i = 0; i < Times; i++)
        {
            int N = Random.Range(0, GList.Count);
            var G = GList[N];
            GList.Remove(G);

            NewList.Add(G.GetComponentInChildren<CardSolt>().CardUid);
        }
        return NewList;
    }
}
