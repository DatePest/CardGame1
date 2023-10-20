using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Deck_HeroItem : MonoBehaviour,IPointerDownHandler,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    DragDropEvent Alpha = new();
    RectTransform _rectTransform;
    public Deck_HeroEditBox CurrnetInBox;
    public Image image;

    public Transform AfterParent;
    public string Uid;
    public int Position;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    public void EnableSet()
    {
        _rectTransform.SetParent(CurrnetInBox.transform);
        _rectTransform.localPosition = Vector3.zero;
    }
    public void RemoveCurrnetBox()
    {
        if (CurrnetInBox == null) return;
        CurrnetInBox.RemovetHeroItem();
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        AfterParent = transform.parent;
        CurrnetInBox = GetComponentInParent<Deck_HeroEditBox>();
        _rectTransform.SetParent(transform.root);
        _rectTransform.SetAsLastSibling();
        gameObject.GetComponent<Image>().raycastTarget = false;
        Alpha.SetRaycastTarget(this, false);
        Alpha.SetCardAlpha(this, 0.5F);
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.SetParent(AfterParent);
        _rectTransform.localPosition = Vector3.zero;
        Alpha.SetCardAlpha(this, 1F);
        Alpha.SetRaycastTarget(this, true);
    }
}
