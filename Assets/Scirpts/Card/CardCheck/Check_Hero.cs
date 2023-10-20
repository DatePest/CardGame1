using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_Hero", menuName = "SO/CardCheck/Hero")]
public class Check_Hero : SO_CardCheckType
{
    [SerializeField] string HeroID;
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        if (so.HeroID == HeroID)
            return F.ReNox(true);
        return F.ReNox(false);
    }
    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            var so = c.GetComponentInParent<CardSolt>().CardSO;
            if (so.HeroID == HeroID)
            {
                NewList.Add(c);
            }
        }
        return NewList;
    }
}
