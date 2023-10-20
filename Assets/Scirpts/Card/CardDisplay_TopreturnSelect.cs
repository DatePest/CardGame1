using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplay_TopreturnSelect : CardDisplay_CardSelect
{
    [SerializeField] TextMeshProUGUI uGUI;
    int MyNubmer=-1;
    public override void Remove()
    {
        base.Remove();
        Close();
    }
    protected override void  Button(PointerEventData P)
    {
        if (CardgameObject == null) return;
        var Card = CardgameObject.GetComponentInChildren<CardSolt>();
        gameObject.GetComponentInParent<CardSelectUI>().SelectReturn_2(Card.CardUid,this, MyNubmer); ;
    }
    public void SetuGUI(int i)
    {
        uGUI.transform.parent.gameObject.SetActive(true);
        uGUI.text = "" +i;
        MyNubmer = i;
    }
    public void Close()
    {
        uGUI.transform.parent.gameObject.SetActive(false);
        uGUI.text = "";
        MyNubmer = -1;
    }

}
