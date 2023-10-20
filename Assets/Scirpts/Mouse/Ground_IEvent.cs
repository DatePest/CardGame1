using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground_IEvent : MonoBehaviour
{
    SpriteRenderer sprite;
    MapSolt mapSolt;
    Color orignaColor;
    public Color OrignaColor => orignaColor;

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        mapSolt = gameObject.GetComponent<MapSolt>();
        orignaColor = sprite.color;
    }
    public void SetColor_Orignal()
    {
        SetColor(OrignaColor);
    }
    public void SetColor(Color color)
    {
        sprite.color = color;
    }
   
    private void OnMouseEnter()
    {
        //SetColor(Color.red);
        CardGameManager.Instance.MyPlayer.UserMouseManager.EnterMapShow(mapSolt);
        if (mapSolt.Unitsolt.My_Unit == null|| TooltipSystem.Instance.Look == true) return;
        if (CardGameManager.Instance.NeedWait == true) return;
        TooltipSystem.Instance.Tooltip_Unit_Display.Set_Show(mapSolt.Unitsolt.My_Unit);
    }
    private void OnMouseExit()
    {
        CardGameManager.Instance.MyPlayer.UserMouseManager.ExitMapShow();
        if (mapSolt.Unitsolt.My_Unit == null) return;
        //sprite.color = Color.white;
        if (TooltipSystem.Instance.Look == true) return;
        if (TooltipSystem.Instance.Tooltip_Unit_Display.IsDisplayIsActive())
            TooltipSystem.Instance.Tooltip_Unit_Display.Close();
    }

    private void OnMouseDown()
    {
        if (mapSolt.Unitsolt.My_Unit == null) return;
        TooltipSystem.Instance.LookCn(true);
    }
    //private void OnMouseDrag()
    //{
       
    //}
    //public void OnDrop(PointerEventData eventData)
    //{
    //    //var T = GetHero(); 
    //    //if (T == null) return;
    //    //eventData.pointerDrag.TryGetComponent<CardSolt>(out var C);
    //    //AbilityNeedData data = new();
    //    //data.UserTarget =T;
    //    //C.UseCard(data);
    //    //C.CardSolt_UseEndToDisdeck();
    //}

    
}
