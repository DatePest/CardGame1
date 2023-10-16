using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class PleyerUITrigger : MonoBehaviour
{
    [SerializeField] GameObject  EndButtonOBJ, EX_Yes,EX_No , EX_Select, EX_Select1, EX_Select2,TS_Wex, TS_Dex, TS_Null;
    //CardSolt EX_ReturnAction =null;
    Action<TS_Tpye> TsAction;
    Action<int> EX_ReturnAction;

    PlayerOBJ MyPlayer;
    //public bool TS_SelectDisplayIsActive()
    //{
    //    return TS_Wex.transform.parent.gameObject.activeSelf;
    //}
    //void Awake()
    //{
    //    AddButtonTrigger(EndButtonOBJ, EndButton);
    //    AddButtonTrigger(EX_Yes, Card_EX_YesButton);
    //    AddButtonTrigger(EX_No, Card_EX_NoButton);
    //    AddButtonTrigger(EX_Select1, EX_Select1Button);
    //    AddButtonTrigger(EX_Select2, EX_Select2Button);
    //    AddButtonTrigger(TS_Wex, TS_WexButton);
    //    AddButtonTrigger(TS_Dex, TS_DexButton);
    //    AddButtonTrigger(TS_Null, TS_NullButton);
    //    EX_Yes.transform.parent.gameObject.SetActive(false);
    //    EndButtonOBJ.SetActive(false);
    //    EX_Select.SetActive(false);
    //    TS_Wex.transform.parent.gameObject.SetActive(false);
    //    TsAction = null;
    //    MyPlayer = GetComponentInParent<PlayerOBJ>();
    //}
    //void AddButtonTrigger(GameObject g, Action<PointerEventData> action)
    //{
    //    EventTrigger eventTrigger = g.AddComponent<EventTrigger>();
    //    EventTrigger.Entry onButton = new EventTrigger.Entry();
    //    onButton.eventID = EventTriggerType.PointerDown;
    //    onButton.callback.AddListener((data) => { action((PointerEventData)data); });
    //    eventTrigger.triggers.Add(onButton);
    //}
    //void EndButton(PointerEventData P)
    //{
    //    if (CardGameManager.Instance.MyPlayer.IsUsingCard) return;
    //    if (!CardGameManager.Instance.GameTurnSystem.CheckCurrentPlayer(MyPlayer)) return;
    //    //EndButtonOBJ.SetActive(false);
    //    CardGameManager.Instance.CardGame_Ctrl.GameStateUpdateServerRpc();
    //}
    //public void SetActiveEndButton(bool b)
    //{
    //    EndButtonOBJ.SetActive(b);
    //}
    //void Card_EX_YesButton(PointerEventData P)
    //{
    //    EX_ReturnAction(1);
    //    EX_Yes.transform.parent.gameObject.SetActive(false);
    //    EX_ReturnAction  = null;
    //}
    //void Card_EX_NoButton(PointerEventData P)
    //{
    //    EX_ReturnAction(2);
    //    EX_Yes.transform.parent.gameObject.SetActive(false);
    //    EX_ReturnAction  = null;
    //}
    //void EX_Select1Button(PointerEventData P)
    //{
    //    EX_ReturnAction(1);
    //    EX_Select.SetActive(false);
    //    EX_ReturnAction  = null;
    //}
    //void EX_Select2Button(PointerEventData P)
    //{
    //    EX_ReturnAction(2);
    //    EX_Select.SetActive(false);
    //    EX_ReturnAction  = null;
    //}
    //public void Card_EX_Button(Action<int> Action)
    //{
    //    EX_ReturnAction  = Action;
    //    EX_Yes.transform.parent.gameObject.SetActive(true);
    //}

    //public void SetActiveEX_Select(Action<int> Action,bool Ex1, bool Ex2)
    //{
    //    EX_ReturnAction  = Action;
    //    EX_Select.SetActive(true);
    //    EX_Select1.SetActive(false);
    //    EX_Select2.SetActive(false);
    //    if (Ex1) EX_Select1.SetActive(true);
    //    if (Ex2) EX_Select2.SetActive(true);
    //}
    //public void SetActiveEX_Select_SetText(string T1 , string T2)
    //{
    //    var TextMeshProUGUI = EX_Select1.GetComponentInChildren<TextMeshProUGUI>();
    //    var TextMeshProUGU2 = EX_Select2.GetComponentInChildren<TextMeshProUGUI>();

    //    TextMeshProUGUI.text = T1;
    //    TextMeshProUGU2.text = T2;
    //}
    //public void SetActiveTS_Select(Action<TS_Tpye> tsAction, bool CanSelectNull)
    //{
    //    TsAction += tsAction;
    //    TS_Wex.transform.parent.gameObject.SetActive(true);
    //    if(CanSelectNull ==true)
    //        TS_Null.SetActive(true);
    //    else
    //        TS_Null.SetActive(false);
    //}
    //void TS_WexButton(PointerEventData P)
    //{
    //    TsAction?.Invoke(TS_Tpye.WEX);
    //    TsAction = null;
    //    TS_Wex.transform.parent.gameObject.SetActive(false);
    //}
    //void TS_DexButton(PointerEventData P)
    //{
    //    TsAction?.Invoke(TS_Tpye.DEX);
    //    TsAction = null;
    //    TS_Wex.transform.parent.gameObject.SetActive(false);
    //}
    //void TS_NullButton(PointerEventData P)
    //{
    //    TsAction?.Invoke(TS_Tpye.AVG); 
    //    TsAction = null;
    //    TS_Wex.transform.parent.gameObject.SetActive(false);
    //}
}
