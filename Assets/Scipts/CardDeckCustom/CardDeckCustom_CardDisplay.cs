using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDeckCustom_CardDisplay : MonoBehaviour
{
    [SerializeField] Image CardArt, CarAttributeTypeIcon, CardAbilityFrame;
    [SerializeField] TextMeshProUGUI CardName, CardDes1;
    EventTrigger eventTrigger;
    public SO_CardBase CardSO { get; private set; }
    public void SetCard(SO_CardBase SO)
    {
        gameObject.SetActive(true);
        CardSO = SO;
        //var Card = G.GetComponentInChildren<CardSolt>().cardSO;
        CardArt.sprite = CardSO.cardArt;
        CardName.text = CardSO.cardname;
        CardDes1.text = CardSO.SkillDepiction;
        //CardDes2.text = Card.SkillDepiction;
        CardAbilityFrame.sprite = GameManager.Instance.DataBase.CardAbilityFrame.GetCardAbilityFrame(SO.CardAttributesType);
        CardAbilityFrame.gameObject.SetActive(true);
        CarAttributeTypeIcon.sprite = GameManager.Instance.DataBase.CardAbilityFrame.GetcardAbilityIcons(SO.CardAttributesType);
        
    }

    public void AddButtonTrigger(Action<CardDeckCustom_CardDisplay> a)
    {
        if (eventTrigger == null)
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry onButton = new EventTrigger.Entry();
        onButton.eventID = EventTriggerType.PointerDown;
        onButton.callback.RemoveAllListeners();
        onButton.callback.AddListener((data) =>
        {
            a(this);
        });
        eventTrigger.triggers.Add(onButton);
    }


    


}
