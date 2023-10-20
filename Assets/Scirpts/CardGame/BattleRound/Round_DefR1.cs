using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Round_DefR1 : Round_IState
{
    public Round_DefR1(Round_IState nextState) : base(nextState)
    {
    }
    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_DefR1;
    }
    public override void StateEnter()
    {
        CurrentPlayer = gameTurnSystem_Round.Players[1];
        CurrentPlayer.SetRule(Enum_Skill_Rule.Attack, false);
        if (NetworkManager.Singleton.IsHost)
            CurrentPlayer.IsEndDiscardCheck = true;
        base.StateEnter();
    }

    public override void StateExit()
    {
        CurrentPlayer.SetRule(Enum_Skill_Rule.Attack, true);
        base.StateExit();
    }

    public override void StateUpdate()
    {
      // null
    }
}
