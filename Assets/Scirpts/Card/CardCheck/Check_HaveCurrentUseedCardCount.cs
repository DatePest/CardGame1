using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_HaveCurrentUseedCardCount", menuName = "SO/CardCheck/Check_HaveCurrentUseedCardCount")]
public class Check_HaveCurrentUseedCardCount : SO_CardCheckType
{
  
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        if(so.Abilities_1.Count < 0) return false;
       foreach(var a in so.Abilities_1)
        {
            foreach (var B in a.skillUseChecks)
            {
                if(B is UseCheck_CurrentUseedCardCount)
                {
                    //Debug.Log("Check_HaveCurrentUseedCardCount true");
                    return F.ReNox(true);
                }
                   
                
            }
        }
        //Debug.Log("Check_HaveCurrentUseedCardCount false");
        return F.ReNox(false);
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