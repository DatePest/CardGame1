using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitSolt : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image CardArt;
    [SerializeField] TextMeshProUGUI Atk, Arrmor,HP,Sh ,Skill ;
    [SerializeField] bool ShowDescription = false;
    public Unit My_Unit{ get; private set; }

    void Awake()
    {
        if (canvas != null)
            canvas.gameObject.SetActive(false);
        Skill.gameObject.SetActive(false);
    }
    public void SetUnit(Unit U )
    {
        if (My_Unit != null) ReMoveUnit();

        canvas.gameObject.SetActive(true);
        My_Unit = U;
        My_Unit.transform.SetParent(this.transform);
        My_Unit.UnitData.SetCallUpdataAction(SoltUpdate, true);
        CardArt.sprite = My_Unit.UnitData.cardArt;
        My_Unit.UnitData.CallUpdata();
        SoltUpdate();

    }
    public void ReMoveUnit()
    {
        CardArt.sprite = null;
        Skill.text = "";
        My_Unit.transform.SetParent(null);
        canvas.gameObject.SetActive(false);
        if (My_Unit == null) return;
        My_Unit.UnitData.SetCallUpdataAction(SoltUpdate, false);
        My_Unit.SetCurrentMapSolt(null);
        My_Unit = null;
       
    }
    public void SoltUpdate()
    {
        Atk.text = $"{My_Unit.UnitData.LastAtk}";
        Arrmor.text = $"{My_Unit.UnitData.LastArmor}";
        HP.text = $"{My_Unit.UnitData.UnitNowHp}/{My_Unit.UnitData.LastMaxHP}";
        Sh.text = $"{My_Unit.UnitData.UnitShield}";
    }
    public void Tool_DisplayUnit(SO_Unit U)
    {
        //var T = ScriptableObject.CreateInstance<SO_Unit>();
        CardArt.sprite = U.cardArt;
        Atk.text = $"{U.LastAtk}";
        Arrmor.text = $"{U.LastArmor}";
        HP.text = $"{U.UnitNowHp}/{U.LastMaxHP}";
        Sh.text = $"{U.UnitShield}";
        if (ShowDescription)
        {
            if (Skill.gameObject.activeSelf == false) Skill.gameObject.SetActive(true);
            Skill.text = U.UnitDepiction;
        }
            
    }
    public void DeckCustom_DisplayUnit(SO_Unit U)
    {

        GetComponentInParent<Show_Swich>().AllDis();
        gameObject.SetActive(true);
        //transform.SetAsLastSibling();
        CardArt.sprite = U.cardArt;
        Atk.text = $"{U.UnitAtk}";
        Arrmor.text = $"{U.UnitArmor}";
        HP.text = $"{U.UnitMaxHp}";
        if (ShowDescription)
        {
            if (Skill.gameObject.activeSelf == false) Skill.gameObject.SetActive(true);
            Skill.text = U.UnitDepiction;
        }
    }

}