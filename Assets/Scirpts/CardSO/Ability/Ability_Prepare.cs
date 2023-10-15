using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Prepare", menuName = "SO/AbilityBase/Ability_Prepare")]
public class Ability_Prepare : AbilityBase
{//èÄîı
    [SerializeField] RoundTimeBuffSO buffSO;
    public override void AddUseAbility(SO_Unit unit)
    {
        unit.SetIsBanAttack(true);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetIsBanAttack(false);
        unit.AddBuff(buffSO);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
