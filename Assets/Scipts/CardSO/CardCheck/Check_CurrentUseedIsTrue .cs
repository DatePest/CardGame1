using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_CurrentUseedIsTrue", menuName = "SO/CardCheck/Check_CurrentUseedIsTrue")]
public class Check_CurrentUseedIsTrue : SO_CardCheckType
{
  
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>();
        if(so.CardSO.Abilities_1.Count < 0) return false;
       foreach(var a in so.CardSO.Abilities_1)
        {
            foreach (var B in a.skillUseChecks)
            {
                if(B is UseCheck_CurrentUseedCardCount)
                {
                    var C = B as UseCheck_CurrentUseedCardCount;
                    return C.Check(so.CurrentUsePlayer); 
                }
                
            }
        }
        return false;
    }

    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> N = new();
       foreach (var a in GList)
        {
            if (CheckCard(a))
                N.Add(a);
        }
        return N;
    }
}