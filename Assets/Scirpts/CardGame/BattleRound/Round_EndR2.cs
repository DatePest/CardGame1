using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Round_EndR2 : Round_IState
{   
   
    public Round_EndR2(Round_IState nextState) : base(nextState)
    {
    }
    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_EndR2;
    }
    public override void StateEnter()
    {
        CurrentPlayer = gameTurnSystem_Round.Players[1];
        Round_ActionInvoke();
        if (NetworkManager.Singleton.IsHost)
            CurrentPlayer.RoundEnd_ClientRpc(CurrentPlayer.NetworkObject);
    }

    public override void StateExit()
    {
    }

    public override void StateUpdate()
    {
      // null
    }
}
