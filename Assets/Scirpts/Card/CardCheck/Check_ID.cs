using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_ID", menuName = "SO/CardCheck/ID")]
public class Check_ID : SO_CardCheckType
{
    [SerializeField] string CardType;

    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        if (so.CardId == CardType)
            return F.ReNox(true);
        return F.ReNox(false);
    }

    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            var so = c.GetComponentInParent<CardSolt>().CardSO;
            if (so.CardId == CardType)
            {
                NewList.Add(c);
            }
        }
        return NewList;
    }
}
