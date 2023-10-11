using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_HaveEx", menuName = "SO/CardCheck/Check_HaveEx")]
public class Check_HaveEx : SO_CardCheckType
{
  
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        if (so.EX1_Wex.Count > 0)
        {
            return true;
        }
        if (so.EX2_Dex.Count > 0)
        {
            return F.ReNox(true);
        }
        return F.ReNox(false);
    }
    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            var so = c.GetComponentInParent<CardSolt>().CardSO;
           
            if (so.EX1_Wex.Count > 0)
            {
                foreach (var a in so.EX1_Wex)
                {
                    NewList.Add(c);
                    continue;
                }
            }
            if (so.EX2_Dex.Count > 0)
            {
                foreach (var a in so.EX2_Dex)
                {
                    NewList.Add(c);
                    continue;
                }
            }
        }
        return NewList;
    }
}
