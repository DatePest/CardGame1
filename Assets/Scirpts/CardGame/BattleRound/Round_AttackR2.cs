using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_AttackR2 : Round_IState
{
    public Round_AttackR2(Round_IState nextState) : base(nextState)
    {
    }
    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_AttackR2;
    }
    public override void StateEnter()
    {
        CurrentPlayer = gameTurnSystem_Round.Players[0];
        //CurrentPlayer.CanSpawnLook = true;
        CurrentPlayer.SetRule(Enum_Skill_Rule.ActionTimes, false);
        CurrentPlayer.IsEndDiscardCheck = true;
        base.StateEnter();
    }

    public override void StateExit()
    {
        //CurrentPlayer.CanSpawnLook = false;
        CurrentPlayer.SetRule(Enum_Skill_Rule.ActionTimes, true);
        base.StateExit();
    }
    public override void StateUpdate()
    {
      // null
    }
}
