using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitCheck_AnyTrue", menuName = "SO/UnitCheck/UnitCheck_AnyTrue")]
public class UnitCheck_AnyTrue : UnitCheckBase
{
    [SerializeField] List<UnitCheckBase> CList;

    public override  bool UseCheck(Unit unit)
    {
        bool B = false;
        foreach (var a in CList)
        {
            B = a.UseCheck(unit);
            if (B == true) break;
        }
        return F.ReNox(B);
    }
}
