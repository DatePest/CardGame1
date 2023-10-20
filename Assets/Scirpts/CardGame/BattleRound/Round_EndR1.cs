using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Round_EndR1 : Round_IState
{   
   
    public Round_EndR1(Round_IState nextState) : base(nextState)
    {
    }
    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_EndR1;
    }
    public override void StateEnter()
    {
        CurrentPlayer = gameTurnSystem_Round.Players[0];
        Round_ActionInvoke();
        if(NetworkManager.Singleton.IsHost)
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
