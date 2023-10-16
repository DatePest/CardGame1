using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.Netcode;
using System;

public class CardSolt : MonoBehaviour
{
    CardDisplay MyCardDisplay;
    public CardSoltInThe CardCurrentInThe { get; private set; } = CardSoltInThe.Null;
    public CardSoltInThe CardBeforeInThe { get; private set; } = CardSoltInThe.Null;
    public bool IsBan { get; private set; } = false;
    public bool IsOwner { get; private set; } = false;
    public bool DisCardUseingLook { get; private set; } = false;
    public bool CardUseingLook { get; private set; } = false;
    public bool EndToRomve { get; private set; } = false;
    public int CardUid { get; private set; } = -1;
    public byte? OwnerID=null;
    int UseCardCheckTimes=0 , DisCardCheckTimes = 0;
    //Coroutine UseCardSkill;
    public SO_CardBase CardSO { get; private set; }
    public PlayerOBJ CurrentPlayer;//CurrentPlayer
    
    public void SetCardSoltInThe(CardsPileEnum cardsPile)
    {
        CardBeforeInThe = CardCurrentInThe;
        switch (cardsPile)
        {
            case CardsPileEnum.hand:
                CardCurrentInThe =CardSoltInThe.InHand;
                break;
            case CardsPileEnum.deck:
                CardCurrentInThe =CardSoltInThe.Indeck;
                break;
            case CardsPileEnum.disdeck:
                CardCurrentInThe =CardSoltInThe.InDisdeck;
                break;
            default:
                throw new ArgumentNullException("CardsPileEnum Is Null");
        }
    }
    public CardsPileEnum CurrentInCardsPile()
    {
        switch (CardCurrentInThe)
        {
            case CardSoltInThe.Indeck:
                return CardsPileEnum.deck;
            case CardSoltInThe.InDisdeck:
                return CardsPileEnum.disdeck;
            case CardSoltInThe.InHand:
                return CardsPileEnum.hand;
            default:
                throw new ArgumentNullException("CardsPileEnum Is Null");
        }
    }

    private void Awake()
    {
        MyCardDisplay = GetComponentInChildren<CardDisplay>();
    }

    public PlayerOBJ CurrentUsePlayer { get
        {
            if (CurrentPlayer == null)
            {
                //Debug.Log("CurrentPlayer == null");
                if (CardGameManager.Instance.MyPlayer.OwnerClientId == OwnerID.Value)
                {
                    CurrentPlayer = CardGameManager.Instance.MyPlayer;
                    return CurrentUsePlayer;
                }
                var T = FindObjectsByType<PlayerOBJ>(FindObjectsSortMode.None);
                //Debug.Log("FindObjectsByType Length =" + T.Length);
                foreach (var a in T)
                {
                    
                    if (a.OwnerClientId == OwnerID.Value)
                    {
                        CurrentPlayer = a;
                            return CurrentUsePlayer;
                    }
                }
                throw new System.NotImplementedException("CurrentPlayer Find is not");
            }

            
            return CurrentPlayer;
        }//private set { CurrentPlayer = value; }
        }

    #region CardDisPlay
    
    public void RemoveCardSO()
    {
        CardSO = null;
        MyCardDisplay.RemoveCardDisplay();
        //OwnerID = null;
    }
    public void DisEnabledCard()
    {
        MyCardDisplay.Set_ChidRootActive(false);
    }
    public void UpdateCardSO(SO_CardBase Card)
    {
        CardSO = Card;
        MyCardDisplay.SetCardDisplay(CardSO);
    }
    public void SetCardSO(SO_CardBase Card )
    {
        MyCardDisplay.Set_ChidRootActive(true);
        UpdateCardSO(Card);
        
        if(CardCurrentInThe == CardSoltInThe.Indeck)
            MyCardDisplay.Set_ChidRootActive(false);
    }
    public void EnableCard(bool IsAllEnable = false)
    {
        DisEnabledCard();
        if (IsAllEnable)
        {
            MyCardDisplay.Set_ChidRootActive(true);
            return;
        }
        if (IsOwner)
        {
            MyCardDisplay.Set_ChidRootActive(true);
        }
        // ChidRoot.SetActive(true); //TestOnly  Allopen
    }

    public void SetIsBan(bool b)
    {
        IsBan = b;
        MyCardDisplay.Set_IsBanArt(b);
    }

    public void SetUseArt(bool b)
    {
        CardUseingLook = b;
        MyCardDisplay.Set_UseArt(b);
    }
    #endregion
    public void SetOwner(bool T) => IsOwner = T;
    public void SetCardUid(int T) => CardUid = T;
    public void SetOwnerID( byte id) { OwnerID = id; }
    public void SetEndToRomve(bool b) => EndToRomve=b;

    public void NetUseStartSkill(Net_AbilityNeedData Netdata)
    {
        AbilityNeedData data = new(Netdata);
        foreach (var a in CardSO.Abilities_1)
        {
            if (a.AbilityID == data.AbilityID)
            {
                CurrentUsePlayer.UseStartSkillAction(data, a);
                return;
            }
        }
        if(CardSO.EX1_Wex.Count>0)
        foreach (var a in CardSO.EX1_Wex)
        {
            if (a.AbilityID == data.AbilityID)
            {
                CurrentUsePlayer.UseStartSkillAction(data, a);
                return;
            }
        }
        if (CardSO.EX2_Dex.Count > 0)
            foreach (var a in CardSO.EX2_Dex)
        {
            if (a.AbilityID == data.AbilityID)
            {
                CurrentUsePlayer.UseStartSkillAction(data, a);
                return;
            }
        }
    }
    public void NetUseSkill(Net_AbilityNeedData Netdata)
    {
        AbilityNeedData data  = new(Netdata);
        foreach(var a in CardSO.Abilities_1)
        {
            if (a.AbilityID == data.AbilityID)
            {
                a.UseSkill(data);
                return;
            }
        }
        if (CardSO.EX1_Wex.Count > 0)
            foreach (var a in CardSO.EX1_Wex)
        {
            if (a.AbilityID == data.AbilityID)
            {
                a.UseSkill(data);
                return;
            }
        }
        if (CardSO.EX2_Dex.Count > 0)
            foreach (var a in CardSO.EX2_Dex)
        {
            if (a.AbilityID == data.AbilityID)
            {
                a.UseSkill(data);
                return;
            }
        }
        if (CardSO.DiscardAbilities.Count > 0)
            foreach (var a in CardSO.DiscardAbilities)
            {
                if (a.AbilityID == data.AbilityID)
                {
                    a.UseSkill(data);
                    return;
                }
            }
    }

   
    public void CardSolt_UseEndToDisdeck()
    {
        CurrentUsePlayer.BeforeData = null;
        SetUseArt(false);
        CurrentUsePlayer.AddActionTimes(-1);
        if (EndToRomve)
        {
            if (!CardGameManager.Instance.IsSingleplayer)
                CardGameManager.Instance.GameNotifyAction_Net.CardUseEndNotifyServerRpc(CardUid, CurrentInCardsPile(), CardsPileEnum.Remove);
            CurrentUsePlayer.cardSpawnScript.CardsPileChange(gameObject,CurrentInCardsPile(), CardsPileEnum.Remove);
          
        }
        else
        {
            if (!CardGameManager.Instance.IsSingleplayer)
                CardGameManager.Instance.GameNotifyAction_Net.CardUseEndNotifyServerRpc(CardUid, CurrentInCardsPile(), CardsPileEnum.disdeck);
            CurrentUsePlayer.cardSpawnScript.CardsPileChange(gameObject, CurrentInCardsPile(), CardsPileEnum.disdeck);
           
        }
        CurrentUsePlayer.SetCurrentRoundUseCardsConut(1);
        //CardUseingLook = false;
        CurrentUsePlayer.SetCurrentUseCard(null,false);
    }
   
    public void UseDisCardAbilities()
    {
        if (CardSO == null) return;
        if (DisCardUseingLook == true) return;
        if (CardSO.DiscardAbilities.Count < 1) return;
        //Debug.Log("UseDisCardAbilities");
        StartCoroutine(Card_DisSkill());
    }
    public void ExCost_SetValueTsTimes(bool IsEx1)
    {
        int i = (CurrentUsePlayer.ExCostDown + CurrentUsePlayer.ExCostDownKeepRound);
        if (IsEx1)
            i = Math.Max (CardSO.EX1_Wex_Cost - i,0);
        else
            i = Math.Max(CardSO.EX2_Dex_Cost - i, 0); 
        if (IsEx1)
        CardGameManager.Instance.Ts_Manager.SetValueTsTimes(+ i);
        else
        CardGameManager.Instance.Ts_Manager.SetValueTsTimes(- i);
        CurrentUsePlayer.SetExCostDown_Zero();

    }
    public void CardCheckTimesADD(int i)=> UseCardCheckTimes += i;
    public void DisCardCheckTimesADD(int i) => DisCardCheckTimes += i;

    IEnumerator Card_DisSkill()
    {
        DisCardUseingLook = true;
        int CompletedNeedCheckTims = 0, NowCheckTims = 0;
        DisCardCheckTimes = 0;
        CompletedNeedCheckTims = CardSO.DiscardAbilities.Count;
        //CardGameManager.Instance.GameNotifyAction_Net.CardUseNotifyServerRpc(cardSO.CardId, CurrentInCardsPile());
        yield return new WaitForSeconds(1f);
        if (true)
        {
            while (DisCardCheckTimes != CompletedNeedCheckTims)
            {
                foreach (var a in CardSO.DiscardAbilities)
                {
                    NowCheckTims++;
                    if (a.Skill_Rule != null)
                    {
                        if (!CurrentUsePlayer.CheckRule((Enum_Skill_Rule)a.Skill_Rule))
                        {
                            DisCardCheckTimes++;
                            continue;
                        }
                    }
                    a.Card_ReadySkill(this, 0.1f, true);

                    Debug.Log(a.AbilityID);
                    while (DisCardCheckTimes != NowCheckTims)
                    {
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
        }
        DisCardUseingLook = false;
    }
    public void UseCard()
    {
        if (CardGameManager.Instance.NeedWait == true) return;
        if (!IsOwner) return;
        if (CardGame_PlayerUIManager.Instance.Get_CardSelectManager().IsDisplay() == true) return;
        if (CardGame_PlayerUIManager.Instance.TS_SelectDisplayIsActive() == true) return;
        if (!CurrentUsePlayer.cardSpawnScript.SpawnEnd) return;
        if (CardSO == null) return;
        if (CardCurrentInThe != CardSoltInThe.InHand) return;
        if (CurrentUsePlayer.IsCanSpawn != true || CurrentUsePlayer.IsUsingCard != false) return;
        //CardUseingLook = true;
        CurrentUsePlayer.SetCurrentUseCard(this ,true);
        SetUseArt(true);
        StartCoroutine(Card());
    }

    bool GetCanEx1Check() => (CurrentUsePlayer.BanWEx || CardSO.CanEx1Check());
    bool GetCanEx2Check() => (CurrentUsePlayer.BanDEx || CardSO.CanEx2Check());
    IEnumerator Card()
    {
        int CompletedNeedCheckTims, NowCheckTims;
        CardGameManager.Instance.GameNotifyAction_Net.CardUseNotifyServerRpc(CardUid, CurrentInCardsPile());
        CradUseing Skill_1 = CradUseing.Waiting, Skill_2 = CradUseing.Waiting, EX_Check = CradUseing.Waiting;
        CurrentUsePlayer.Event_UseCard(this); 
        yield return new WaitForSeconds(1f);
        while (CardGameManager.Instance.NeedWait == true)
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (Skill_1 == CradUseing.Waiting)
        {
            NowCheckTims = 0;
            UseCardCheckTimes = 0;
            CompletedNeedCheckTims = CardSO.Abilities_1.Count;
            while (UseCardCheckTimes != CompletedNeedCheckTims)
            {
                foreach (var a in CardSO.Abilities_1)
                {
                    NowCheckTims++;
                    if (a.Skill_Rule != Enum_Skill_Rule.Null)
                    {
                        if (!CurrentUsePlayer.CheckRule((Enum_Skill_Rule)a.Skill_Rule))
                        {
                            UseCardCheckTimes++;
                            continue;
                        }
                    }
                    a.Card_ReadySkill(this, 0.1f);
                    while (UseCardCheckTimes != NowCheckTims)
                    {
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
            //Skill_1 = CradUseing.Completed;
            if (!GetCanEx1Check()&& !GetCanEx2Check())
            {
                EX_Check = CradUseing.Completed;
                Skill_2 = CradUseing.Completed;
            }
        }
        int UseEX = 0;
        if (EX_Check == CradUseing.Waiting)
        {
            UseCardCheckTimes = 0;
            CardGame_PlayerUIManager.Instance.Card_EX_Button(CardCheckTimesADD);
            while (UseCardCheckTimes == 0)
            {
                yield return new WaitForSeconds(1f);
            }
            if (UseCardCheckTimes == 1) // turn
            {
                UseCardCheckTimes = 0;
                CardGame_PlayerUIManager.Instance.SetActiveEX_Select(CardCheckTimesADD, GetCanEx1Check(), GetCanEx2Check());
                CardGame_PlayerUIManager.Instance.SetActiveEX_Select_SetText("Wex", "Dex");
                while (UseCardCheckTimes == 0)
                {
                    yield return new WaitForSeconds(0f);
                }
                if (UseCardCheckTimes == 1)
                {
                    UseEX = 1;
                    ExCost_SetValueTsTimes(true);
                    if (CardGameManager.Instance.IsSingleplayer != true)
                        CardGameManager.Instance.GameNotifyAction_Net.ExCost_SetValueTsTimesServerRpc(CardUid, true);
                    //CardGameManager.Instance.tS_Manager.SetValueTsTimes(-cardSO.EX1_Wex_Cost);
                }
                else if (UseCardCheckTimes == 2)
                {
                    UseEX = 2;
                    ExCost_SetValueTsTimes(false);
                    if (CardGameManager.Instance.IsSingleplayer != true)
                        CardGameManager.Instance.GameNotifyAction_Net.ExCost_SetValueTsTimesServerRpc(CardUid, false);
                    //CardGameManager.Instance.tS_Manager.SetValueTsTimes(+cardSO.EX2_Dex_Cost);
                }

            }
            else if (UseCardCheckTimes == 2) // f
            {
                Skill_2 = CradUseing.Completed;
               // EX_Check = CradUseing.Completed;
            }
        }
        if (Skill_2 == CradUseing.Waiting)
        {
            CurrentUsePlayer.Event_EXSkillUse_ServerRpc(CurrentUsePlayer, CardUid, CurrentInCardsPile());
            NowCheckTims = 0;
            UseCardCheckTimes = 0;
            List<SO_SKillAbility> T = new();
            if (UseEX == 1)
                T = CardSO.EX1_Wex;
            else
                T = CardSO.EX2_Dex;
            CompletedNeedCheckTims = T.Count;
            while (UseCardCheckTimes != CompletedNeedCheckTims)
            {
                foreach (var a in T)
                {
                    NowCheckTims++;
                    if (a.Skill_Rule != Enum_Skill_Rule.Null)
                        if (!CurrentUsePlayer.CheckRule((Enum_Skill_Rule)a.Skill_Rule))
                        {
                            UseCardCheckTimes++;
                            continue;
                        }

                    a.Card_ReadySkill(this, 0.1f);
                    while (UseCardCheckTimes != NowCheckTims)
                    {
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
            CardGameManager.Instance.GameNotifyAction_Net.CardExUseEndServerRpc();
        }
        CardSolt_UseEndToDisdeck();
    }
}
public enum CardSoltInThe
{
    Null,InHand,Indeck,InDisdeck,InRemove
}

public enum CradUseing
{
    Waiting,Completed
}
