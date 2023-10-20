using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_CurrentUseed", menuName = "SO/CardCheck/CurrentUseed")]
public class Check_CurrentUseed : SO_CardCheckType
{
    [SerializeField] UseCheck_CurrentUseedCardCount Checkobj;

    public override bool CheckCard(GameObject G)
    {
        var so = G.GetComponentInParent<CardSolt>();
        return F.ReNox(Checkobj.Check(so.CurrentUsePlayer));
    }

    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        return GList;
    }
}
