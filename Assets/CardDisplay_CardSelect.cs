using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplay_CardSelect : MonoBehaviour
{
    [SerializeField] protected Image CardArt, CarAttributeTypeIcon, CardAbilityFrame;
    [SerializeField] protected TextMeshProUGUI CardName, CardDes1,InThe;
    protected EventTrigger eventTrigger;
    protected GameObject CardgameObject;

    public void SetCard(GameObject G)
    {
        CardgameObject = G;
        var Card = G.GetComponentInChildren<CardSolt>();
        DisplayCard(Card.CardSO);
        var T = Card.CurrentInCardsPile();
        if (InThe != null)
        InThe.text = CardsPileEnumToText.ToText(T);

        //CardDes2.text = Card.SkillDepiction;
        
        AddButtonTrigger();
    }



    public void DisplayCard(SO_CardBase cardBase)
    {
        CardArt.sprite = cardBase.cardArt;
        CardName.text = cardBase.cardname;
        CardDes1.text = cardBase.SkillDepiction;
        CardAbilityFrame.sprite = GameManager.Instance.DataBase.CardAbilityFrame.GetCardAbilityFrame(cardBase.CardAttributesType);
        CardAbilityFrame.gameObject.SetActive(true);
        CarAttributeTypeIcon.sprite = GameManager.Instance.DataBase.CardAbilityFrame.GetcardAbilityIcons(cardBase.CardAttributesType);
    }
    
    public virtual void Remove()
    {
        if (eventTrigger == null) return;
        eventTrigger.triggers.Clear();
        CardgameObject = null;
    }
    protected void OnDisable()
    {
        Remove();
    }
    protected void AddButtonTrigger()
    {
        if (eventTrigger == null)
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry onButton = new EventTrigger.Entry();
        onButton.eventID = EventTriggerType.PointerDown;
        onButton.callback.AddListener((data) => { Button((PointerEventData)data); });
        eventTrigger.triggers.Add(onButton);
    }
    protected virtual void Button(PointerEventData P)
    {
        if (CardgameObject == null) return;
        var Card = CardgameObject.GetComponentInChildren<CardSolt>();
        gameObject.GetComponentInParent<CardSelectManager>().SelectReturn_1(Card.CardUid, this.gameObject); ;
    }

}
