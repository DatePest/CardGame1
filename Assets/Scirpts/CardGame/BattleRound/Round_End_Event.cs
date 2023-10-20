using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_End_Event : Round_IState
{   
   
    public Round_End_Event(Round_IState nextState) : base(nextState)
    {
    }
    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_End_Event;
    }
    public override void StateEnter()
    {
        Round_ActionInvoke();
        CardGameManager.Instance.GameTurnSystem.GameStateUpdate();
    }

    public override void StateExit()
    {
    }

    public override void StateUpdate()
    {
      // null
    }
}
