using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGame_PlayerUIManager : Singleton_T_Mono<CardGame_PlayerUIManager>
{
    [SerializeField] UI_EventAdd EndButton,UseEx, EX_Select, TS_AddSelect;
    [SerializeField] SkillSelectTooltip skillselectTooltip;
    [SerializeField] CardSelectUI cardSelectManager;
    Dictionary<byte, Action<GameObject>> ButtonAction =new();
    Action<int> EX_ReturnAction;
    Action<TS_Tpye> TsAction;
    private void Awake()
    {
        base.Awake();
        EndButton = Instantiate(EndButton, transform, false);
        UseEx = Instantiate(UseEx, transform, false);
        EX_Select = Instantiate(EX_Select, transform, false);
        TS_AddSelect = Instantiate(TS_AddSelect, transform, false);
        skillselectTooltip = Instantiate(skillselectTooltip, transform, false);
        cardSelectManager = Instantiate(cardSelectManager, transform, false);
        ButtonAction.Add((byte)ButtonAction.Count, End);
        ButtonAction.Add((byte)ButtonAction.Count, Card_EX_YesButton);
        ButtonAction.Add((byte)ButtonAction.Count, Card_EX_NoButton);
        ButtonAction.Add((byte)ButtonAction.Count, EX_Select1Button);
        ButtonAction.Add((byte)ButtonAction.Count, EX_Select2Button);
        ButtonAction.Add((byte)ButtonAction.Count, TS_WexButton);
        ButtonAction.Add((byte)ButtonAction.Count, TS_DexButton);
        ButtonAction.Add((byte)ButtonAction.Count, TS_NullButton);
    }

    public void UseButtonAction(byte i,GameObject g)
    {
        ButtonAction[i].Invoke(g);
    }

    public SkillSelectTooltip Get_SkillselectTooltip() => skillselectTooltip;
    public CardSelectUI Get_CardSelectManager() => cardSelectManager;
    //void AddButtonTrigger(GameObject g, Action<PointerEventData> action)
    //{
    //    EventTrigger eventTrigger = g.AddComponent<EventTrigger>();
    //    EventTrigger.Entry onButton = new EventTrigger.Entry();
    //    onButton.eventID = EventTriggerType.PointerDown;
    //    onButton.callback.AddListener((data) => { action((PointerEventData)data); });
    //    eventTrigger.triggers.Add(onButton);
    //}
    void End(GameObject g)
    {
        if (CardGameManager.Instance.MyPlayer.IsUsingCard) return;
        if (!CardGameManager.Instance.GameTurnSystem.CheckCurrentPlayer(CardGameManager.Instance.MyPlayer)) return;
        //EndButtonOBJ.SetActive(false);
        CardGameManager.Instance.CardGame_Ctrl.GameStateUpdateServerRpc();
    }
    public void SetActiveEndButton(bool b)
    {
        EndButton.gameObject.SetActive(b);
    }
    void Card_EX_YesButton(GameObject g )
    {
        EX_ReturnAction(1);
        g.SetActive(false);
        EX_ReturnAction = null;
    }
    void Card_EX_NoButton(GameObject g)
    {
        EX_ReturnAction(2);
        g.SetActive(false);
        EX_ReturnAction = null;
    }
    void EX_Select1Button(GameObject g)
    {
        EX_ReturnAction(1);
        g.SetActive(false);
        EX_ReturnAction = null;
    }
    void EX_Select2Button(GameObject g)
    {
        EX_ReturnAction(2);
        g.SetActive(false);
        EX_ReturnAction = null;
    }
    public void Card_EX_Button(Action<int> Action)
    {
        EX_ReturnAction = Action;
        UseEx.gameObject.SetActive(true);
    }

    public void SetActiveEX_Select(Action<int> Action, bool Ex1, bool Ex2)
    {
        EX_ReturnAction = Action;
        EX_Select.gameObject.SetActive(false);
        EX_Select.GetButton(0).gameObject.SetActive(false);
        EX_Select.GetButton(1).gameObject.SetActive(false);
        if (Ex1) EX_Select.GetButton(0).gameObject.SetActive(true); 
        if (Ex2) EX_Select.GetButton(1).gameObject.SetActive(true);
    }
    public void SetActiveEX_Select_SetText(string T1, string T2)
    {
        var TextMeshProUGUI = EX_Select.GetButton(0).gameObject.GetComponentInChildren<TextMeshProUGUI>();
        var TextMeshProUGU2 = EX_Select.GetButton(1).gameObject.GetComponentInChildren<TextMeshProUGUI>();

        TextMeshProUGUI.text = T1;
        TextMeshProUGU2.text = T2;
    }
    public void SetActiveTS_Select(Action<TS_Tpye> tsAction, bool CanSelectNull)
    {
        TsAction += tsAction;
        TS_AddSelect.gameObject.SetActive(true);
        if (CanSelectNull == true)
            TS_AddSelect.GetButton(2).gameObject.SetActive(true);
        else
            TS_AddSelect.GetButton(2).gameObject.SetActive(false);
    }
    void TS_WexButton(GameObject g)
    {
        TsAction?.Invoke(TS_Tpye.WEX);
        TsAction = null;
        g.SetActive(false);
    }
    void TS_DexButton(GameObject g)
    {
        TsAction?.Invoke(TS_Tpye.DEX);
        TsAction = null;
        g.SetActive(false);
    }
    void TS_NullButton(GameObject g)
    {
        TsAction?.Invoke(TS_Tpye.AVG);
        TsAction = null;
        g.SetActive(false);
    }

    public bool TS_SelectDisplayIsActive()
    {
        return TS_AddSelect.gameObject.activeSelf;
    }

  
}
