using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillSelectTooltip : TooltipBase
{
    [SerializeField] TextMeshProUGUI uGUI;
    public void Show_Set(SelectTargetTpye selectTarget,TargetRange range)
    {

        uGUI.text = $" {TargetRangeTpye(range)} {TargetTpye(selectTarget)}";
        gameObject.SetActive(true);
    }

    string TargetTpye(SelectTargetTpye tpye)
    {
        switch (tpye)
        {
            case SelectTargetTpye.Map:
                return "’nš¤";
            case SelectTargetTpye.Unit:
                return "šdˆÊ";
        }
        return null;
    }
    string TargetRangeTpye(TargetRange tpye)
    {
        switch (tpye)
        {
            case TargetRange.All:
                return "™Ô•û";
            case TargetRange.Enemy:
                return "“G•û";
            case TargetRange.Own:
                return "‰ä•û";
        }
        return null;
    }

    public override void Ins()
    {
        //throw new System.NotImplementedException();
    }
}
public enum SelectTargetTpye
{
    Unit,Map
}
