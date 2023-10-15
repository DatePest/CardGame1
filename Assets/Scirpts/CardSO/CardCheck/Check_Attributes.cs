using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_Attributes", menuName = "SO/CardCheck/Attributes")]
public class Check_Attributes : SO_CardCheckType
{
    [SerializeField] CardAttributesType CardType;
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        if (so.CardAttributesType == CardType)
            return F.ReNox(true);
        return F.ReNox(false);
    }
    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            var so = c.GetComponentInParent<CardSolt>().CardSO;
            if (so.CardAttributesType == CardType)
            {
                NewList.Add(c);
            }
        }
        return NewList;
    }
}
