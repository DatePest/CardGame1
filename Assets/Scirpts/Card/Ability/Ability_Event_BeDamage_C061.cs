using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_BeDamage_C061", menuName = "SO/AbilityBase/BeDamage_C061")]
public class Ability_Event_BeDamage_C061 : BeDamage_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;
    [SerializeField] Event_Ability_RemoveBuff Remove;
    Round_State_Enum state_Enum = Round_State_Enum.Round_Battle;
    protected override void IEvent(SO_Unit HurtUnit, SO_Unit AtkUnit, int arg3)
    {
        if(CardGameManager.Instance.GameTurnSystem.GetCurrentRoundState() == state_Enum)
        {

            AddbuffBase.IEvent(HurtUnit);
            Remove.IEvent(HurtUnit);
        }
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
