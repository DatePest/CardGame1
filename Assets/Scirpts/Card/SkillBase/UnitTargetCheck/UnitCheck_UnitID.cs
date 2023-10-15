using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitCheck_UnitID", menuName = "SO/UnitCheck/UnitCheck_UnitID")]
public class UnitCheck_UnitID : UnitCheckBase
{
    [SerializeField] SO_Unit Unit;

    public override  bool UseCheck(Unit unit)
    {
        if (unit.UnitData.UintID== Unit.UintID)
            return F.ReNox(true);
        return F.ReNox(false);
    }
}
