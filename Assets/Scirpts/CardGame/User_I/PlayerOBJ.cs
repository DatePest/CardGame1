using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Unity.Netcode;
using System.Threading.Tasks;

public class PlayerOBJ : NetworkBehaviour
{
    [SerializeField] List<Vector3> positions, rotation;
    public NetworkVariable<NetworkString> UserName = new("NewPlayer");
    public Camera Usercamera { get; private set; }
    public Canvas canvas { get; private set; }
    public int ActionTimes { get; private set; }
    public int EndNeedDiscard { get; private set; }
    public int CurrentHandCardsConut => cardSpawnScript.FindCardsPile(CardsPileEnum.hand).Cards.Count;
    public int TargetDiscardCards { get; private set; }
    public int CurrentRoundUseedCardsConut { get; private set; }
    public int ExCostDown { get; private set; } = 0;//â∫àÍéüégóp
    public int ExCostDownKeepRound { get; private set; } = 0; //êÆå¬âÒçáç~í·
    public int RoundStartAddedCard { get; private set; } = 0;
    public CardSolt CurrentUseCard { get; private set; }

    public bool IsCanSpawn, IsEndDiscardCheck; //CanSpawnLook = false
    public bool IsUsingCard { get; private set; }
    public Dictionary<string, bool> Rule { get; private set; }
    public MouseManager UserMouseManager { get; private set; }
    [SerializeField] GameObject MyGround;
    public CardSpawnScript cardSpawnScript { get; private set; }
    public bool BanWEx { get; private set; } = false;
    public bool BanDEx { get; private set; } = false;
    public event Action<AbilityNeedData, SO_SKillAbility> UseStartSkill;
    public event Action<GameObject> UseEXSkill,UseCard;
    public event Action<int> SkillDisCardEvent;
    public int SkillDisCardCount { get; private set; } = 0;
    public AbilityNeedData? BeforeData=null;
    void Awake()
    {
        Usercamera = GetComponentInChildren<Camera>();
        canvas = GetComponentInChildren<Canvas>();
        UserMouseManager = GetComponentInChildren<MouseManager>();
        Rule = new();
        foreach (Enum_Skill_Rule value in Enum.GetValues(typeof(Enum_Skill_Rule)))
        {
            string V = value.ToString();
            bool B = true;
            Rule.Add(V, B);
        }

    }
    private void Start()
    {
        if(!IsOwner)
        {
            UserMouseManager.enabled = false;
            Usercamera.gameObject.SetActive(false);
            canvas.gameObject.SetActive(false);
            return;
        }
        SetCamera((int)OwnerClientId);
        CardGameManager.Instance.SetmyPlayer(this);
       
    }
    public void CardSpawnScript_FindSetLoacl()
    {
        cardSpawnScript = CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)OwnerClientId];
        if (IsOwner)
        cardSpawnScript.IsOwner = true;
    }
    public void SetCurrentUseCard(CardSolt c,bool b)
    {
        CurrentUseCard = c;
        IsUsingCard = b;
    }
    public void SetExCostDown_Zero()
    {
        ExCostDown = 0;
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetCostDown(ExCostDown);
    }
    public void UseStartSkillAction(AbilityNeedData data, SO_SKillAbility sObj_SKill)
    {
        UseStartSkill?.Invoke(data,sObj_SKill);
    }
    public void UseSkillDisCardEvent(int i)
    {
        //Debug.Log("UseSkillDisCardEvent");
        SkillDisCardCount += i;
        SkillDisCardEvent?.Invoke(i);
    }
    public void SetExCostDown(int i)
    {
        ExCostDown += i;
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetCostDown(ExCostDown);
    }
    public void SetExCostDownKeepRound(int i)
    {
        ExCostDownKeepRound += i;
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetCostDownRound(ExCostDownKeepRound);
    }
    public void SetRoundStartAddedCard(int i)
    {
        RoundStartAddedCard += i;
    }
    public void SetBanWEx(bool b)
    {
        BanWEx = b;
    }
    public void SetBanDEx(bool b)
    {
        BanDEx = b;
    }
    public void SetCurrentReset()
    {
        CurrentRoundUseedCardsConut = 0;
        TargetDiscardCards = 0;
        ActionTimes = 0;
        ExCostDown = 0;
        SkillDisCardCount = 0;
        ExCostDownKeepRound = 0;
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetActionTime(ActionTimes);
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetCostDown(ExCostDown);
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetCostDownRound(ExCostDownKeepRound);
        BanWEx = false;
        BanDEx = false;

    }
    [ServerRpc(RequireOwnership = false)]
    public void Event_EXSkillUse_ServerRpc(NetworkBehaviourReference playerobj ,int Cardid,CardsPileEnum cardsPile,ServerRpcParams serverRpcParams = default)
    {
        Event_EXSkillUse_ClientRpc(playerobj,Cardid, cardsPile);
    }
    [ClientRpc]
     void Event_EXSkillUse_ClientRpc(NetworkBehaviourReference playerobj, int Cardid, CardsPileEnum cardsPile)
    {
        playerobj.TryGet(out PlayerOBJ P);
        var C =P.cardSpawnScript.FindCard(Cardid, cardsPile);
        UseEXSkill?.Invoke(C);
    }

    public void Event_UseCard(CardSolt card)
    {
        UseCard?.Invoke(card.gameObject);
        if(!CardGameManager.Instance.IsSingleplayer)
        Event_UseCard_ServerRpc( card.CardUid, card.CurrentInCardsPile());
    }
    [ServerRpc(RequireOwnership = false)]
    void Event_UseCard_ServerRpc(int Cardid, CardsPileEnum cardsPile, ServerRpcParams serverRpcParams = default)
    {
        var id = serverRpcParams.Receive.SenderClientId;
        Event_UseCard_ClientRpc( Cardid, cardsPile,id);
    }
    [ClientRpc]
    void Event_UseCard_ClientRpc( int Cardid, CardsPileEnum cardsPile ,ulong SenderClientId)
    {
        if (NetworkManager.Singleton.LocalClientId == SenderClientId) return;
        var C = cardSpawnScript.FindCard(Cardid, cardsPile);
        UseCard?.Invoke(C);
    }
    public void SetCurrentRoundUseCardsConut(int i , bool UseRpc =true)
    {
        CurrentRoundUseedCardsConut = Math.Max(0, CurrentRoundUseedCardsConut+=i);
        //Debug.Log("CurrentRoundUseedCardsConut = " + CurrentRoundUseedCardsConut);
        if (!CardGameManager.Instance.IsSingleplayer&& UseRpc)
            SetCurrentRoundUseCardsConut_ServerRpc(i);
    }
    [ServerRpc(RequireOwnership = false)]
    void SetCurrentRoundUseCardsConut_ServerRpc(int i, ServerRpcParams serverRpcParams = default)
    {
        var id = serverRpcParams.Receive.SenderClientId;
        SetCurrentRoundUseCardsConut_ClientRpc(i, id);
    }
    [ClientRpc]
    void SetCurrentRoundUseCardsConut_ClientRpc(int i, ulong SenderClientId)
    {
        if (NetworkManager.Singleton.LocalClientId == SenderClientId) return;
        SetCurrentRoundUseCardsConut(i, false);
    }
    public void SetCamera(int i)
    {
        int N=0;
        if (i != 0) N = 1;
        Usercamera.gameObject.transform.parent.localPosition = positions[N];
        Usercamera.gameObject.transform.parent.localRotation = Quaternion.Euler(rotation[N]);
        CardGameManager.Instance.CardSpawnManager.GetComponent<Canvas>().worldCamera = Usercamera;

    }

    public void SetRule(Enum_Skill_Rule R, bool B)
    {
        Rule[R.ToString()] = B;
        NetworkString S = R.ToString();
        bool Bool = B;
        SetRuleServerRpc(S, Bool, NetworkObjectId);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SetRuleServerRpc(NetworkString s, bool b, ulong currentObjId) => SetRuleClientRpc(s, b, currentObjId);
    [ClientRpc]
    public void SetRuleClientRpc(NetworkString S, bool b, ulong currentObjId)
    {
        if (NetworkObjectId != currentObjId) return;
        Rule[S.ToString()] = b;
    }
    //[ClientRpc]
    //public void RoundStartClientRpc(int i)
    //{
    //    cardSpawnScript.SpawnSizeFromToTarget(((i - CurrentHandCardsConut) + RoundStartAddedCard), CardsPileEnum.deck, CardsPileEnum.hand);
    //    RoundStartAddedCard = 0;
    //}
    public void RoundStart(int i)
    {
        cardSpawnScript.SpawnSizeFromToTarget(((i - CurrentHandCardsConut) + RoundStartAddedCard), CardsPileEnum.deck, CardsPileEnum.hand);
        RoundStartAddedCard = 0;
    }

    public bool CheckRule(Enum_Skill_Rule R)
    {
        //if(Rule[R.ToString()] == false)
        //{
        //    return false
        //}

        return Rule[R.ToString()] ;
    }

    public  void AddActionTimes(int i)
    {
        //CanSpawnLook == true&&
        if ( IsCanSpawn == false) return;
        ActionTimes += i;
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetActionTime(ActionTimes);
        if (ActionTimes <= 0)
        {
            IsCanSpawn = false;
        }
        else
            IsCanSpawn = true;
    }
    public void SetActionTimes(int i)
    {
        ActionTimes = i;
        CardGameManager.Instance.GameSceneUI.playerTooltip.SetActionTime(ActionTimes);
    }
    

    [ClientRpc]
    public void RoundEnd_ClientRpc(NetworkObjectReference networkObject )
    {
        networkObject.TryGet(out var B);
        if (B.NetworkObjectId != this.NetworkObjectId) return;
        if (!IsOwner) return;
        if (CurrentHandCardsConut > 6) TargetDiscardCards = CurrentHandCardsConut-6;
        if (TargetDiscardCards < EndNeedDiscard) TargetDiscardCards = EndNeedDiscard;
        if(TargetDiscardCards ==0) 
            CardGameManager.Instance.CardGame_Ctrl.GameStateUpdateServerRpc();
        else
        RoundEndRun(TargetDiscardCards,false);
    }


     async void RoundEndRun(int Times,bool IsRandom = false)
    {
        CardsPileEnum From = CardsPileEnum.hand;
        CardsPileEnum Target = CardsPileEnum.disdeck;
        AbilityNeedData data = new();
        data.CurrentUsePlayer = this;
        if (cardSpawnScript.FindCardsPile(From).Cards.Count > Times)
        {
            var SearchEndCards = cardSpawnScript.FindCardsPile(From).Cards;
            if (SearchEndCards.Count > 1)
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
                await cardSpawnScript.FindCardGoto(data.SelectCardUids, From, Target);
                /////////////////////////////////////////////////////////////////
                while (!cardSpawnScript.SpawnEnd)
                {
                    await Task.Delay(300);
                }
            }
        }
        CardGameManager.Instance.CardGame_Ctrl.GameStateUpdateServerRpc();

    }
    public void SetEndDiscard(int i)
    {
        EndNeedDiscard = i;
    }
       
}
