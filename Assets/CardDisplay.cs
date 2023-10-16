using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] GameObject ChidRoot;
    [SerializeField] Image CardArt, CarAttributeTypeIcon, CardBack, CardAbilityFrame, Useing, BanArt;
    [SerializeField] TextMeshProUGUI CardName, CardDes1;
    // Start is called before the first frame update
    void Start()
    {
        ChidRoot.SetActive(false);

    }
    public void RemoveCardDisplay()
    {
        //if (ChidRoot != null)
        //    ChidRoot.SetActive(true);
        CardArt.sprite = null;
        CardName.text = null;
        CardDes1.text = null;
        CardAbilityFrame.sprite = null;
        CarAttributeTypeIcon.sprite = null;
        ChidRoot.SetActive(false);
    }

    public virtual void SetCardDisplay(SO_CardBase Card)
    {
        CardArt.sprite = Card.cardArt;
        CardName.text = Card.cardname;
        CardDes1.text = Card.SkillDepiction;
        //CardDes2.text = Card.SkillDepiction;
        CardAbilityFrame.sprite = GameManager.Instance.DataBase.CardAbilityFrame.GetCardAbilityFrame(Card.CardAttributesType);
        CarAttributeTypeIcon.sprite = GameManager.Instance.DataBase.CardAbilityFrame.GetcardAbilityIcons(Card.CardAttributesType);
    }
    public void Set_ChidRootActive(bool b)
    {
        ChidRoot.SetActive(b);
    }
    public void Set_IsBanArt(bool b)
    {
        BanArt.gameObject.SetActive(b);
    }
    public void Set_UseArt(bool b)
    {
        Useing.gameObject.SetActive(b);
    }
}
