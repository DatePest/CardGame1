using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip_CardDisplay : TooltipBase
{
    [SerializeField] CardDisplay_CardSelect display;

    public override void Ins()
    {
        //throw new System.NotImplementedException();
    }

    public void Set_Show(SO_CardBase cardBase)
    {
        TooltipSystem.Instance.CloseAll();

        display.DisplayCard(cardBase);
        gameObject.SetActive(true);
    }

}
