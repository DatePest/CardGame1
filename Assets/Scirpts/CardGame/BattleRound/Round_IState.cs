using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public abstract class Round_IState : I_StatePattern
{
    protected PlayerOBJ CurrentPlayer;
    public Round_IState NextState;
    public GameTurnSystem_Round gameTurnSystem_Round;
    public  Action Round_Action;
    public Round_IState(Round_IState nextState)
    {
        NextState = nextState;
    }
    public override void StateEnter()
    {
        CurrentPlayer.IsCanSpawn = true;
        CurrentPlayer.SetActionTimes(1);
        CurrentPlayer.SetCurrentRoundUseCardsConut(-99,false);
        CurrentPlayer.PlayerTrigger.SetActiveEndButton(true);
        Round_ActionInvoke();
        //Debug.Log("CurrentPlayer Is _" + CurrentPlayer.UserName.Value);
    }
    public override void StateExit()
    {
        CurrentPlayer.IsCanSpawn = false;
        CurrentPlayer.PlayerTrigger.SetActiveEndButton(false);
        CurrentPlayer.IsEndDiscardCheck = false;

        //CardGameManager.Instance.GameTurnSystem_Net.EndButtonClientRpc(CurrentPlayer.OwnerClientId,false);
    }
    public abstract Round_State_Enum GetRound_State_Enum();
    public  void Round_ActionInvoke()
    {
        Round_Action?.Invoke();
    }
    public bool IsCurrentPlayer(PlayerOBJ P)
    {
        if (CurrentPlayer == P)
            return true;
        return false;
    }

}
