using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStatetip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ActionTime, CostDownRound, CostDown;
    [SerializeField] string Text_ActionTime = "çsìÆéüù…:", Text_CostDownRound = "âÒçáè¡ñ’å∏è≠:", Text_CostDown = "â∫éüè¡ñ’å∏è≠:";

    private void Awake()
    {
        SetActionTime(0);
        SetCostDownRound(0);
        SetCostDown(0);
    }
    public void SetActionTime(int T)
    {
        ActionTime.text = Text_ActionTime + T;
    }
    public void SetCostDownRound(int T)
    {
        CostDownRound.text = Text_CostDownRound + T;
    }
    public void SetCostDown(int T)
    {
        CostDown.text = Text_CostDown + T;
    }
}
