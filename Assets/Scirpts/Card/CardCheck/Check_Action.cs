using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_Action", menuName = "SO/CardCheck/Action")]
public class Check_Action : SO_CardCheckType
{
    [SerializeField] CardType CardType;
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        if (so.cardType == CardType)
            return F.ReNox(true);
        return F.ReNox(false);
    }
    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            var so = c.GetComponentInParent<CardSolt>().CardSO;
            if (so.cardType == CardType)
            {
                NewList.Add(c);
            }
        }
        return NewList;
    }
}
