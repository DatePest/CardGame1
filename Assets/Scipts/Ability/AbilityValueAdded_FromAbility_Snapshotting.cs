using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ValueAdded_Snapshotting_", menuName = "SO/AbilityBase/ValueAdded_FromAbility_Snapshotting")]
public class AbilityValueAdded_FromAbility_Snapshotting : AbilityBase
{
    [SerializeField]
    AbilityTpye FromTpye, AddedTarget;
    [SerializeField]
    int AddedTimes=0;
    [SerializeField]
    float AbilityMultiplier=1;

    int LastModfir;

    /// <summary>
    /// âıè∆ã@êß ˙ùâ¡·cëOèCê≥ìI % ù…
    /// </summary>
    /// <param name="unit"></param>
    public override void AddUseAbility(SO_Unit unit)
    {
        int i = AbilityBaseAPI.GetAbility(unit,FromTpye);
        var a = (i * AbilityMultiplier) + AddedTimes;
        LastModfir = (int)a;
        AbilityBaseAPI.GetAbilitySetFunc(unit, LastModfir, AddedTarget);
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        AbilityBaseAPI.GetAbilitySetFunc(unit, -LastModfir, AddedTarget);
    }


    public override void Stack(bool IsAdd)
    {
        return;
    }

}

