using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_AttackR1 : Round_IState
{   
   
    public Round_AttackR1(Round_IState nextState) : base(nextState)
    {
    }

    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_AttackR1;
    }

    public override void StateEnter()
    {
        CurrentPlayer = gameTurnSystem_Round.Players[0];
        base.StateEnter();
    }

    public override void StateExit()
    {
        base.StateExit();
    }

    public override void StateUpdate()
    {
      // null
    }
}
