using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Card_Drag_Drop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    private RectTransform _rectTransform;
    CardSolt cardBase;
    DragDropEvent Alpha=new();

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        cardBase = GetComponentInParent<CardSolt>();
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {

        //Camera.main.ScreenToWorldPoint(eventData.position); Ázä∑ç¿ïW
        _rectTransform.SetAsLastSibling();
        //_rectTransform.SetParent(transform.parent);
        gameObject.GetComponent<Image>().raycastTarget = false;
        //Debug.Log("OnBeginDrag");
        Alpha.SetRaycastTarget(this,false);
        Alpha.SetCardAlpha(this,0.5F);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.SetParent(eventData.pointerDrag.transform.parent);
        _rectTransform.localPosition = Vector3.zero;
        //Debug.Log("OnEndDrag");
        Alpha.SetCardAlpha(this, 1F);
        Alpha.SetRaycastTarget(this,true); 
        //if(cardBase.CurrentHasPlayer.IsCanSpawn !=false) UseCrad(eventData);

    }
    void UseCrad(PointerEventData eventData)
    {
        // List<RaycastResult> results = new List<RaycastResult>();
        //var position = cardBase.CurrentHasPlayer.Usercamera.ScreenToWorldPoint(eventData.position);
        //Ray ray = cardBase.CurrentHasPlayer.Usercamera.ScreenPointToRay(position);

        //EventSystem.current.RaycastAll(eventData, results);
        Ray ray = cardBase.CurrentUsePlayer.Usercamera.ScreenPointToRay(eventData.position);
        RaycastHit[] Hits = Physics.RaycastAll(ray);
        foreach (var result in Hits)
        {
            if(result.transform.TryGetComponent<MapSolt>(out var T))
            {
                Unit U;
                if(T.GetUnit() != null)
                {
                    U = T.GetUnit();
                    AbilityNeedData data = new();
                    data.UserTarget = U;
                    //cardBase.UseCard(data);
                    //data.AbilityToTarget = a.;
                }
                break;
            }
            continue;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        cardBase.UseCard();
        //cardBase.SelectDiscard();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (cardBase.IsOwner != true)
        {
            if(cardBase.CardCurrentInThe != CardSoltInThe.InDisdeck)
                return;
        }
        if (cardBase.CardCurrentInThe == CardSoltInThe.Indeck ||cardBase.CardCurrentInThe == CardSoltInThe.InRemove) return;
        if (cardBase.CardSO  == null) return;
       TooltipSystem.Instance.Tooltip_CardDisplay.Set_Show(cardBase.CardSO);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(TooltipSystem.Instance.Tooltip_CardDisplay.IsDisplayIsActive())
        TooltipSystem.Instance.Tooltip_CardDisplay.Close();
    }
}

