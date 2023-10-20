using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
using System.Threading.Tasks;

public class CardSpawnScript : MonoBehaviour
{
    //[SerializeField] int handSize;
    //[SerializeField] int deckSize;
    //Canvas canvas;
    [SerializeField]
     CardsPile hand, deck, disdeck, search , Remove;
    bool IsSpawnExit =false;
    Coroutine SpawnCoroutine;
    public bool SpawnEnd { get; private set; } = false;
    public bool InSynchronizeIng { get; private set; } = false;
    bool SysWaitSynchronize = false;
    public bool IsOwner=false;
    public byte? OwnerID = null;

    public List<CardSolt> AllCard { get; private set; } = new();
    // List<string> PlayerDeck, PlayerHand, PlayerDisDeck, PlayerRemoveDeck, PlayerSearchDeck ;
    public PlayerOBJ MyPlayerUser { get; private set ; }
    [SerializeField] GameObject cardPrefab;

    private void Awake()
    {
        //PlayerDeck = new();
        //PlayerHand = new();
        //PlayerDisDeck = new();
        //PlayerRemoveDeck = new();
        //PlayerSearchDeck = new();
    }
    public async void HeroDeadRemove(SO_Unit unit)
    {
        List<GameObject> Cards;
        CardsPileEnum P;
        //
        P = CardsPileEnum.hand;
        Cards = Find_HeroID(unit.UintID, P);
        await ListCardsPileChange(Cards, P, CardsPileEnum.Remove);
        //
        P = CardsPileEnum.deck;
        Cards = Find_HeroID(unit.UintID, P);
        await ListCardsPileChange(Cards, P, CardsPileEnum.Remove);
        //
        P = CardsPileEnum.disdeck;
        Cards = Find_HeroID(unit.UintID, P);
        await ListCardsPileChange(Cards, P, CardsPileEnum.Remove);

    }
    public List<GameObject> Find_HeroID(string Name, CardsPileEnum From)
    {
        List<GameObject> Cards = new();
        var C = FindCardsPile(From).Cards;
        foreach (var a in C)
        {
            var t = a.GetComponentInChildren<CardSolt>();
            if (t.CardSO.HeroID == Name)
            {
                Cards.Add(a);
            }
        }
        return Cards;
    }




    public async void SynchronizeCurrentDeck(CardsPileEnum cardsPile , int[] CardUIDs)
    {
        if (InSynchronizeIng != false)
        {
            await Task.Delay(300);
        }
        InSynchronizeIng = true;
        // var List = FindDeckCardSoltList(cardsPile);
        //var CP = FindCardsPile(cardsPile);
        //InstantiateCardSO(CP.Cards, List);
        var CSs = FindDeckCardSoltList(cardsPile);
        int TampInt;
        SO_CardBase TampSO;
        for(int i = 0; i < CSs.Count; i++)
        {
            if (CSs[i].CardUid == CardUIDs[i]) continue;
            for (int A = i + 1; A < CSs.Count; A++)
            {
                if (CSs[A].CardUid == CardUIDs[i])
                {
                    TampInt = CSs[i].CardUid;
                    TampSO = CSs[i].CardSO;
                    CSs[i].SetCardUid(CSs[A].CardUid);
                    CSs[i].SetCardSO(CSs[A].CardSO);
                    CSs[A].SetCardUid(TampInt);
                    CSs[A].SetCardSO(TampSO);
                    break;
                }
            }
        }

        InSynchronizeIng = false;
        if (SysWaitSynchronize == true) SysWaitSynchronize = false;
    }
    public void InstantiateGoto(CardsPileEnum cardsPile, int SetCardUid, string CardId)
    {
        var d = FindCardsPile(cardsPile);
        var card = Instantiate(cardPrefab, transform, false);
        var cardSolt = card.GetComponentInChildren<CardSolt>();
        if (IsOwner) cardSolt.SetOwner(true) ;
        d.Add(card, false);
        cardSolt.SetCardUid(SetCardUid);
        cardSolt.SetCardSoltInThe(cardsPile);
        if (CardId!=null)    InstantiateCardSO(card, CardId);
    }
    //public void InstantiateGoto_for(CardsPileEnum cardsPile,int Size , int start)
    //{
    //    for(int i = start; i < Size+start; i++)
    //    {
    //        InstantiateGoto(cardsPile,i);
    //    }
    //}
    //public void InstantiateGoto_List(CardsPileEnum cardsPile,List<string> ListDeck )
    //{
    //    foreach (var a in ListDeck)
    //    {
    //        InstantiateGoto(cardsPile,a);
    //    }
    //}
    void InstantiateCardSO(List<GameObject> G, List<string> Deck)
    {
        for (int i = 0; i < Deck.Count; i++)
        {
            InstantiateCardSO(G[i], Deck[i]);
        }
    }
    void InstantiateCardSO(GameObject G, string ID )
    {
        if (ID == null || G ==null) return;
        var SO = GameManager.Instance.DataBase.StringToCard(ID);
        var CardSolt = G.GetComponentInChildren<CardSolt>();
        if (CardSolt.CardSO == null) CardSolt.RemoveCardSO();
        CardSolt.SetCardSO(SO);
        CardSolt.SetOwnerID(OwnerID.Value);

        AllCard.Add(CardSolt);

    }
    void InstantiateCardToCardsPile(string CardId, CardsPileEnum AddGoto)
    {
        var card = Instantiate(cardPrefab, transform, false);
        var CardsPile = FindCardsPile(AddGoto);
        CardsPile.Add(card, false);
    }

    public async Task ListCardsPileChange(List<GameObject> G, CardsPileEnum From, CardsPileEnum Goto)
    {
        foreach(var a in G)
        {
            while (!SpawnEnd)
            {
                await Task.Delay(200);
            }
            CardsPileChange(a, From, Goto);
        }
    }

    public  void CardsPileChange(GameObject G ,CardsPileEnum From, CardsPileEnum Goto)
    {
        var CardSolt = G.GetComponentInChildren<CardSolt>();
        switch (Goto)
        {
            case CardsPileEnum.hand:
                CardSolt.SetCardSoltInThe(Goto);
                CardSolt.EnableCard();
                break;
            case CardsPileEnum.deck:
                CardSolt.SetCardSoltInThe(Goto);
                CardSolt.DisCard();
                break;
            case CardsPileEnum.disdeck:
                CardSolt.SetCardSoltInThe(Goto);
                CardSolt.EnableCard(true);
                break;
            case CardsPileEnum.search:
                CardSolt.EnableCard(true);
                break;
            case CardsPileEnum.Remove:
                CardSolt.EnableCard(true);
                break;
            default:
                throw new ArgumentNullException("CardsPileEnum Is Null");
        }
        FindCardsPile(From).Remove(G);
        FindCardsPile(Goto).Add(G);
        
        //FindDeckCardSoltList(From).Remove(CardSolt.CardSO.CardId);
        //FindDeckCardSoltList(Goto).Add(CardSolt.CardSO.CardId);
    }
    public async Task<List<GameObject>> FindCardGoto(int[] CardIds, CardsPileEnum From, CardsPileEnum Goto)
    {
        List<GameObject> Cards = new() ;
       foreach (var a in CardIds)
       {
            while (!SpawnEnd)
            {
                await Task.Delay(200);
            }
            var T = FindCardGoto(a, From, Goto);
            if(T!=null)
             Cards.Add(T);
       }
        return Cards;
    }
    public GameObject FindCardGoto(int CardId, CardsPileEnum From, CardsPileEnum Goto)
    {
        List<GameObject> Cards = FindCardsPile(From).Cards;
        GameObject Target = null;
        foreach (var a in Cards)
        {
            var t = a.GetComponentInChildren<CardSolt>();
            if (t.CardSO == null) continue;
            if (t.CardUid == CardId && t.CardUseingLook !=true)
            {
                Target = a;
                break;
            }
        }
        if (Target == null) return null;
        CardsPileChange(Target, From, Goto);
        return Target;
    }
    public CardsPile FindCardsPile(CardsPileEnum From)
    {
        CardsPile CardsPile;
        switch (From)
        {
            case CardsPileEnum.hand:
                CardsPile = hand;
                break;
            case CardsPileEnum.deck:
                CardsPile = deck;
                break;
            case CardsPileEnum.disdeck:
                CardsPile = disdeck;
                break;
            case CardsPileEnum.search:
                CardsPile = search;
                break;
            case CardsPileEnum.Remove:
                CardsPile = Remove;
                break;
            default:
                throw new ArgumentNullException("CardsPileEnum Is Null");
        }
        return CardsPile;
    }
    public List<CardSolt> FindDeckCardSoltList(CardsPileEnum cardsPile)
    {
        List<CardSolt> CardSoltS = new();
        var Cards = FindCardsPile(cardsPile).Cards;
        foreach (var a in Cards)
        {

            CardSoltS.Add(a.GetComponentInChildren<CardSolt>());
        }

        //    switch (cardsPile)
        //{
        //    case CardsPileEnum.hand:
        //        List = PlayerHand;
        //        break;
        //    case CardsPileEnum.deck:
        //        List = PlayerDeck;
        //        break;
        //    case CardsPileEnum.disdeck:
        //        List = PlayerDisDeck;
        //        break;
        //    case CardsPileEnum.search:
        //        List = PlayerSearchDeck;
        //        break;
        //    case CardsPileEnum.Remove:
        //        List = PlayerRemoveDeck;
        //        break;
        //    default:
        //        throw new ArgumentNullException("CardsPileEnum Is Null");
        //}
        return CardSoltS;
    }
    public GameObject FindCard(int Uid, CardsPileEnum From)
    {
        var Cards = FindCardsPile(From);
        foreach (var C in Cards.Cards)
        {
            var A = C.GetComponentInChildren<CardSolt>();
            if (A.CardUid == Uid)
            {
                return C;
            }
        }
        return null;
    }
    public  List<GameObject> FindCardSAll(int[] Uid)
    {
        List<GameObject> GL = new();
        foreach(var a in Uid)
        {
            FindCardAll(a);
        }
        return GL;
    }

    public GameObject FindCardAll(int Uid)
    {
        var pilesArray = Enum.GetValues(typeof(CardsPileEnum)) as CardsPileEnum[];
        foreach (var a in pilesArray)
        {
            var G =FindCard(Uid, a);
            if (G != null)
            {
                return G;
            }
        }
        return null;
    }
    //public void FindcardTo(GameObject card , CardsPileEnum Target)
    //{
    //    var pilesArray = Enum.GetValues(typeof(CardsPileEnum)) as CardsPileEnum[];
    //    foreach (var a in pilesArray)
    //    {
    //        var Cards= FindCardsPile(a).Cards;
    //        if (Cards.Contains(card))
    //        {
    //            CardsPileChange(card, a, Target);
    //            return;
    //        }
    //    }
    //}
    public void SpawnSizeFromToTarget(int Size, CardsPileEnum From, CardsPileEnum Target, bool IsDarwCardSO = false)
    {
        if (SpawnCoroutine != null)
            return;
        //Action<CardsPileEnum, CardsPileEnum, bool> Spawnaction = SpawnSizeFromToTarget;
        SpawnCoroutine = StartCoroutine(SpawnTime(Size,  From,  Target, IsDarwCardSO));
    }
    async void ReDeck(List<GameObject> Cards)
    {
        await ListCardsPileChange(Cards, CardsPileEnum.disdeck, CardsPileEnum.deck);
    }
    async void SpawnSizeFromToTarget(CardsPileEnum From, CardsPileEnum Target, bool IsDarwALL = false)
    {
        if (deck.Cards.Count == 0)
        {
            var S = FindCardsPile(CardsPileEnum.disdeck).Cards;
            if (From == CardsPileEnum.deck && S.Count >0 )
            {
                SysWaitSynchronize = true;
                ReDeck(S);
                await Task.Delay(500);

                if (NetworkManager.Singleton.IsHost)
                {
                    CardGameManager.Instance.CardSpawnManager.ClientSynchronizServerRpc(OwnerID.Value, CardsPileEnum.deck, true);
                }
                while (SysWaitSynchronize == true)
                {
                    await Task.Delay(200);
                }
                await Task.Delay(500);
                
            }
            else
            {
                IsSpawnExit = true;
                return;
            }
        }
       
        var Cards = FindCardsPile(From).Cards;
        var card =   Cards[^1];
        CardsPileChange(card, From, Target);
        if (IsDarwALL) card.GetComponentInChildren<CardSolt>().EnableCard(true);
    }

    private IEnumerator SpawnTime(int Size, CardsPileEnum From, CardsPileEnum Target, bool b)
    {
        SpawnEnd = false;
        for (int i = 0; i < Size; i++)
        {
            SpawnSizeFromToTarget(From, Target, b);
            if (IsSpawnExit)
            {
                yield return new WaitForSeconds(0.5F);
                break;
            }
            yield return new WaitForSeconds(0.5F);
        }
        SpawnCoroutine = null;
        IsSpawnExit = false;
        SpawnEnd = true;
    }
    public void Shuffle_Cards(CardsPileEnum cardsPile)
    {
        FindCardsPile(cardsPile).Shuffle_Cards();
        //if (CardGameManager.Instance.IsSingleplayer) return;
        //FindDeckCardSoltList(cardsPile).Shuffle_List();
    }
    
}

    


