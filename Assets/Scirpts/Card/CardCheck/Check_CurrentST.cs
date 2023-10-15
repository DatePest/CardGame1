using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_CurrentST", menuName = "SO/CardCheck/Check_CurrentST")]
public class Check_CurrentST : SO_CardCheckType
{
    [SerializeField] TS_Tpye Tpye;

    public override bool CheckCard(GameObject GList)
    {

        if (CardGameManager.Instance.Ts_Manager.TS_NowTpye == Tpye)
            return F.ReNox(true);
        return F.ReNox(false);
    }

    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        return null;
    }
}
