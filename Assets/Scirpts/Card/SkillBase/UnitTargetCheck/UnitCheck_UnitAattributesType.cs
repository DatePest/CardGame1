using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitCheck_UnitAattributesType", menuName = "SO/UnitCheck/UnitAattributesType")]
public class UnitCheck_UnitAattributesType : UnitCheckBase
{
    [SerializeField] CardAttributesType CheckType;

    public override  bool UseCheck(Unit unit)
    {
        if (unit.UnitData.AttributesType== CheckType)
            return F.ReNox(true);
        return F.ReNox(false);
    }
}
