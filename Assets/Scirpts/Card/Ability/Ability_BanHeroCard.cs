using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_BanHeroCard", menuName = "SO/AbilityBase/BanHeroCard")]
public class Ability_BanHeroCard : AbilityBase
{
    public override void AddUseAbility(SO_Unit unit)
    {
        foreach(var a in unit.CurrentOwner.cardSpawnScript.AllCard)
        {
            if(a.CardSO.HeroID == unit.UintID)
            {
                a.SetIsBan(true);
            }
        }
        
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        foreach (var a in unit.CurrentOwner.cardSpawnScript.AllCard)
        {
            if (a.CardSO.HeroID == unit.UintID)
            {
                a.SetIsBan(false);
            }
        }
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
