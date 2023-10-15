using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitCheck_Buff", menuName = "SO/UnitCheck/UnitCheck_Buff")]
public class UnitCheck_Buff : UnitCheckBase
{
    [SerializeField] RoundTimeBuffSO CheckBuff;

    public override  bool UseCheck(Unit unit)
    {
        if (unit.UnitData.IfHaveBuff(CheckBuff))
            return F.ReNox(true);
        return F.ReNox(false);
    }
}
