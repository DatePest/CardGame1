using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ValueAdded", menuName = "SO/AbilityBase/ValueAdded")]
public class AbilityValueAdded : AbilityBase
{
    public int HP, Arrmor, Attack, Armor_Atk;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.SetModfirMaxHP(HP);
        unit.SetModfirAtk(Attack);
        unit.SetModfirArrmor(Arrmor);
        unit.SetArmorAtk(Armor_Atk);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetModfirMaxHP(-HP);
        unit.SetModfirAtk(-Attack);
        unit.SetModfirArrmor(-Arrmor);
        unit.SetArmorAtk(-Armor_Atk);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
