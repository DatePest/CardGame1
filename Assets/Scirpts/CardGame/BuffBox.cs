using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffBox : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Image art;
    BuffBase buff =null;

    public void SetBox(BuffBase b)
    {
        buff = b;
        art.sprite = b.Icon;
    }
    public void Remove()
    {
        buff = null;
        art.sprite = null; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buff == null) return;
        TooltipSystem.Instance.Tooltip_Text.SetTooltipText(buff.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.Tooltip_Text.Close();
    }
}
