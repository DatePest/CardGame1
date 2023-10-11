using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChangeAattributesType", menuName = "SO/AbilityBase/ChangeAattributesType")]
public class Ability_ChangeAattributesType : AbilityBase
{
    [SerializeField]
    CardAttributesType attributesType;

    CardAttributesType original;
    public override void AddUseAbility(SO_Unit unit)
    {
        original = unit.AttributesType;
        unit.SetAttributesType(attributesType);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetAttributesType(original);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
