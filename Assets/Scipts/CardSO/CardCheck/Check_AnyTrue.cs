using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_AnyTrue", menuName = "SO/CardCheck/AnyTrue")]
public class Check_AnyTrue : SO_CardCheckType
{
    [SerializeField] List<SO_CardCheckType> CList;

    public override bool CheckCard(GameObject GList)
    {
        bool B = false;
        foreach(var a in CList)
        {
            B = a.CheckCard(GList);
            if (B == true) break;
        }
        return F.ReNox(B);
    }

    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            if (CheckCard(c))
            {
                NewList.Add(c);
            }
        }
        return NewList;
    }
}
