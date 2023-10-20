using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Start : Round_IState
{
    public Round_Start(Round_IState nextState) : base(nextState)
    {
    }
    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_Start;
    }
    public override void StateEnter()
    {
        CurrentPlayer = gameTurnSystem_Round.Players[0];
        foreach(var a in gameTurnSystem_Round.Players)
        {
            a.SetCurrentReset();
        }
        gameTurnSystem_Round.Players[0].RoundStart(5);
        gameTurnSystem_Round.Players[1].RoundStart(6);
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
