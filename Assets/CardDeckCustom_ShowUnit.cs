using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDeckCustom_ShowUnit : MonoBehaviour
{
    Deck_HeroEdit_Dropdown _HeroEdit_Dropdown;
    Image _image;
    string Uid;
    void Awake()
    {
        _HeroEdit_Dropdown = gameObject.GetComponentInParent<Deck_HeroEdit_Dropdown>();
        _image = GetComponent<Image>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => {
            _HeroEdit_Dropdown.ShowUnit(Uid);
        });


        EventTrigger trigger = GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }
        trigger.triggers.Add(entry);
    }

    public void SetImage(Sprite sp)
    {
        _image.sprite = sp;
    }
    public void SetUid(string uid)
    {
        Uid = uid;
    }

}
