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
    [SerializeField] TS_script tS_Script;
    Coroutine Notify;
    public string UserName;
    public Camera Usercamera { get; private set; }
    public Canvas canvas { get; private set; }
    //public NetworkVariable<bool> Net_IsCanSpawn_Start = new();// IsEndDiscardCheck_net = new();
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
    List<CardSolt> EndDiscards_Netid;
    public Dictionary<string, bool> Rule { get; private set; }
    public MouseManager UserMouseManager { get; private set; }
    [SerializeField] GameObject MyGround, notifyUseCradOBJ;
    public CardSpawnScript cardSpawnScript { get; private set; }
    public PleyerAddTrigger PlayerTrigger { get; private set; }
    public PlayerFingerGuessing PlayerFinger { get; private set; }
    public CardSelectManager CardSelectManager { get; private set; }
    public SkillSelectTooltip skillSelectTooltip { get; private set; }
    public bool BanWEx { get; private set; } = false;
    public bool BanDEx { get; private set; } = false;
    public PlayerTooltip playerTooltip { get; private set; }
    public event Action<AbilityNeedData, SO_SKillAbility> UseStartSkill;
    public event Action<GameObject> UseEXSkill,UseCard;
    public event Action<int> SkillDisCardEvent;
    public int SkillDisCardCount { get; private set; } = 0;
    public AbilityNeedData? BeforeData=null;
    // Start is called before the first frame update
    void Awake()
    {
        Usercamera = GetComponentInChildren<Camera>();
        canvas = GetComponentInChildren<Canvas>();
        UserMouseManager = GetComponentInChildren<MouseManager>();
        PlayerTrigger = GetComponentInChildren<PleyerAddTrigger>();
        PlayerFinger = GetComponentInChildren<PlayerFingerGuessing>();
        CardSelectManager = GetComponentInChildren<CardSelectManager>();
        skillSelectTooltip = GetComponentInChildren<SkillSelectTooltip>();
        playerTooltip = GetComponentInChildren<PlayerTooltip>();
        PlayerFinger.SetSeletOBJ(this);
        notifyUseCradOBJ.SetActive(false);
        Rule = new();
        //ActionTimes = new();
        //IsCanSpawn = new();
        //IsUsingCard = new();
        foreach (Enum_Skill_Rule value in Enum.GetValues(typeof(Enum_Skill_Rule)))
        {
            string V = value.ToString();
            bool B = true;
            Rule.Add(V, B);
        }

    }
    private void Start()
    {
        //if (NetworkManager.LocalClientId != OwnerClientId)
        if(!IsOwner)
        {
           
            PlayerTrigger.enabled = false;
            //cardSpawnScript.enabled = false;
            UserMouseManager.enabled = false;
            PlayerFinger.gameObject.SetActive(false);
            Usercamera.gameObject.SetActive(false);
            canvas.gameObject.SetActive(false);
            skillSelectTooltip.gameObject.SetActive(false);
            //tS_Script.gameObject.SetActive(false);
            return;
        }
        SetCamera((int)OwnerClientId);
        tS_Script.gameObject.SetActive(true);
        tS_Script.TS_Add();
        CardGameManager.Instance.SetmyPlayer(this);
        //Net_IsCanSpawn_Start.OnValueChanged += IsStartValueChanged;
        CardGameManager.Instance.GameNotifyAction_Net.CardUse += notifyUseCrad;
        CardGameManager.Instance.GameNotifyAction_Net.Unit += notifyUnitSkill;
    }

    public void CardSpawnScript_FindSetLoacl()
    {
        cardSpawnScript = CardGameManager.Instance.CardSpawnManager.CardSpawnScripts[OwnerClientId];
        if(IsOwner)
        cardSpawnScript.IsOwner = true;
    }
    //private void IsStartValueChanged(bool previousValue, bool newValue)
    //{
    //    IsCanSpawn = newValue;
    //    PlayerTrigger.SetActiveEndButton(newValue);
    //    if (IsCanSpawn)  ActionTimes = 1;
    //}
    public void SetCurrentUseCard(CardSolt c,bool b)
    {
        CurrentUseCard = c;
        IsUsingCard = b;
    }
    public void SetExCostDown_Zero()
    {
        ExCostDown = 0;
        playerTooltip.SetCostDown(ExCostDown);
    }
    public void UseStartSkillAction(AbilityNeedData data, SO_SKillAbility sObj_SKill)
    {
        UseStartSkill?.Invoke(data,sObj_SKill);
    }
    public void UseSkillDisCardEvent(int i)
    {
        SkillDisCardCount += 0;
        SkillDisCardEvent?.Invoke(i);
    }
    public void SetExCostDown(int i)
    {
        ExCostDown += i;
        playerTooltip.SetCostDown(ExCostDown);
    }
    public void SetExCostDownKeepRound(int i)
    {
        ExCostDownKeepRound += i;
        playerTooltip.SetCostDownRound(ExCostDownKeepRound);
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
        playerTooltip.SetActionTime(ActionTimes);
        playerTooltip.SetCostDown(ExCostDown);
        playerTooltip.SetCostDownRound(ExCostDownKeepRound);
        BanWEx = false;
        BanDEx = false;

    }
    //[ServerRpc(RequireOwnership = false)]
    //public void SetCurrentResetServerRpc(ServerRpcParams serverRpcParams = default)
    //{
    //    SetCurrentResetClientRpc();
    //}
    //[ClientRpc]
    //public void SetCurrentResetClientRpc()
    //{
    //    SetCurrentReset();
    //}
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
        Debug.Log("CurrentRoundUseedCardsConut = " + CurrentRoundUseedCardsConut);
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
        Usercamera.gameObject.transform.parent.localPosition = positions[i];
        Usercamera.gameObject.transform.parent.localRotation = Quaternion.Euler(rotation[i]);
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
    public void SetRuleServerRpc(NetworkString s, bool b, ulong currentObjId)
    {
        SetRuleClientRpc(s, b, currentObjId);
    }
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
        playerTooltip.SetActionTime(ActionTimes);
        if (ActionTimes <= 0)
        {
            IsCanSpawn = false;
        }
        else
            IsCanSpawn = true;
        //Debug.Log("ActionTimes" + ActionTimes);
        //Debug.Log("IsCanSpawn" + IsCanSpawn);
    }
    public void SetActionTimes(int i)
    {
        ActionTimes = i;
        playerTooltip.SetActionTime(ActionTimes);
    }
    private void OnDisable()
    {
        if (!IsOwner) return;
        CardGameManager.Instance.GameNotifyAction_Net.CardUse -= notifyUseCrad;
        CardGameManager.Instance.GameNotifyAction_Net.Unit -= notifyUnitSkill;
    }

    private void notifyUnitSkill(Unit U, float Time)
    {
        notifyUseCradOBJ.SetActive(true);
        var T = notifyUseCradOBJ.GetComponent<RawImage>();
        if(U.UnitData.cardArt.texture!=null)
        T.texture = U.UnitData.cardArt.texture;
        if (Notify != null)
        {
            StopCoroutine(Notify);
        }
        Notify = StartCoroutine(notifyUseCradWait(Time));
    }

    public void notifyUseCrad(CardSolt currentCradslot, float Time)
    {
        notifyUseCradOBJ.SetActive(true);
        var T =notifyUseCradOBJ.GetComponent<RawImage>();
        if(currentCradslot.CardSO.cardArt != null)
        T.texture = currentCradslot.CardSO.cardArt.texture;
        if(Notify != null)
        {
            StopCoroutine(Notify);
        }
        Notify = StartCoroutine(notifyUseCradWait(Time));
       
    }

    IEnumerator notifyUseCradWait(float T)
    {
        yield return new WaitForSeconds(T);
        notifyUseCradOBJ.SetActive(false);
        Notify = null;
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
            CardGameManager.Instance.GameTurnSystem_Net.GameStateUpdateServerRpc();
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
                    var CS = CardSelectManager;
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
        CardGameManager.Instance.GameTurnSystem_Net.GameStateUpdateServerRpc();

    }
    public void SetEndDiscard(int i)
    {
        EndNeedDiscard = i;
    }
    
    public void ReturnDiscardID(CardSolt cardSolt)
    {
        EndDiscards_Netid.Add(cardSolt);
    }

    
       
}
