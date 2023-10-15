using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_Abilities", menuName = "SO/CardCheck/Abilities")]
public class Check_Abilities : SO_CardCheckType
{
    [SerializeField] SO_SKillAbility CardType;
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        foreach (var a in so.Abilities_1)
        {
            if (a.AbilityID == CardType.AbilityID)
            {
                return F.ReNox(true);
            }
        }
        if (so.EX1_Wex.Count > 0)
        {
            foreach (var a in so.EX1_Wex)
            {
                if (a.AbilityID == CardType.AbilityID)
                {
                    return F.ReNox(true);
                }
            }
        }
        if (so.EX2_Dex.Count > 0)
        {
            foreach (var a in so.EX2_Dex)
            {
                if (a.AbilityID == CardType.AbilityID)
                {
                    return F.ReNox(true);
                }
            }
        }
        return F.ReNox(false);
    }
    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            var so = c.GetComponentInParent<CardSolt>().CardSO;
            foreach (var a in so.Abilities_1)
            {
                if (a.AbilityID == CardType.AbilityID)
                {
                    NewList.Add(c);
                    continue;
                }
            }
            if (so.EX1_Wex.Count > 0)
            {
                foreach (var a in so.EX1_Wex)
                {
                    if (a.AbilityID == CardType.AbilityID)
                    {
                        NewList.Add(c);
                        continue;
                    }
                }
            }
            if (so.EX2_Dex.Count > 0)
            {
                foreach (var a in so.EX2_Dex)
                {
                    if (a.AbilityID == CardType.AbilityID)
                    {
                        NewList.Add(c);
                        continue;
                    }
                }
            }
        }
        return NewList;
    }
}
