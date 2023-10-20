using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip_Ui : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    [TextArea(4, 4)]
    string Tooltip;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Instance.Tooltip_Text.SetTooltipText(Tooltip);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.Tooltip_Text.Close();
    }
}
