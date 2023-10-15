using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck_HeroEditBox : MonoBehaviour,IDropHandler
{
    public Deck_HeroItem HeroItem { get; private set; }

    public int Position { get; private set; }

    public void SetPosition(int i)
    {
        Position = i;
    }

    public void SetHeroItem(Deck_HeroItem heroItem)
    {
      
        HeroItem = heroItem;
        HeroItem.Position = Position;
        HeroItem.CurrnetInBox = this;
        HeroItem.EnableSet();
        HeroItem.transform.SetParent(this.transform);
        HeroItem.transform.localPosition = Vector3.zero;
        HeroItem.gameObject.SetActive(true);
    }
    public void RemovetHeroItem()
    {
        if (HeroItem == null) return;
        HeroItem.Position = -1;
        HeroItem = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var g = eventData.pointerDrag;
        if (g.TryGetComponent<Deck_HeroItem>(out var I))
        {
            I.RemoveCurrnetBox();
            if (HeroItem != null)
            {
                Deck_HeroItem TMP = HeroItem;
                RemovetHeroItem();
                I.CurrnetInBox.SetHeroItem(TMP);

            }
            SetHeroItem(I);
            HeroItem.AfterParent = transform;
           

        }
        
    }
}
