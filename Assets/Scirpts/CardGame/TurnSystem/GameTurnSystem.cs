using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState:byte
{
    Initialization,
    Round,
    Finish
}
public class GameTurnSystem 
{
    public List<PlayerOBJ> Playersorder { get; private set; }
    CardGameManager gameManagerInstance;
      I_StatePattern CurrentGameState;
    public Dictionary<GameState, I_StatePattern> GameSystemState { get; private set; } = new();
    GameTurnSystem_Round _Round ;
    //public List<Playerdeck> Players { get { return CardGameManager.Instance.Players; } }
    public void StateChange(GameState State)
    {
        if (CurrentGameState == GameSystemState[State])
            return;

        if (CurrentGameState != null)
        {
            CurrentGameState.StateExit();
        }
        try
        {
            CurrentGameState = GameSystemState[State];
            gameManagerInstance.GameSceneUI.GameStateUIText.SetTMPro(State.ToString());
            //Debug.Log("Turn_+" + State.ToString());
            CurrentGameState.StateEnter();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
            //Debug.LogError("系統沒有該狀態");
            CurrentGameState = null;
        }
    }
    public GameTurnSystem(CardGameManager gameManagerInstance )
    {
        this.gameManagerInstance = gameManagerInstance;
        GameSystemState.Add(GameState.Initialization, new GameTurnSystem_Initialization(gameManagerInstance));
        GameSystemState.Add(GameState.Round, new GameTurnSystem_Round(gameManagerInstance));
        GameSystemState.Add(GameState.Finish, new GameTurnSystem_Finish(gameManagerInstance));
        _Round = (GameTurnSystem_Round)GameSystemState[GameState.Round];
        Playersorder = new();
        
    }
    
    public void GetRoundiState(Round_State_Enum _State ,  ref Round_IState iState)
    {
        iState =_Round.Round_IStates[_State.ToString()];
    }
    public Round_State_Enum GetCurrentRoundState()
    {
        return _Round.CurrentRound_State.GetRound_State_Enum();
    }
    public bool CheckCurrentPlayer(PlayerOBJ P)
    {
        return _Round.CurrentRound_State.IsCurrentPlayer(P);
    }
    public void GameStateUpdate()
    {
        CurrentGameState.StateUpdate();
    }
    public int  GetCurrentRound()
    {
        return _Round.Curren_Round;
    }

    public void SetPlayersOrder(ulong ID, bool IsFirst)
    {
        PlayerOBJ P = null;
        foreach(var a in gameManagerInstance.Players)
        {
            if (a.OwnerClientId == ID)
            {
                P = a;
                Playersorder.Add(a);
                break;
            } 
        }
        foreach (var a in gameManagerInstance.Players)
        {
            if (a.OwnerClientId != ID)
            {
                Playersorder.Add(a);
            }
        }
        if (!IsFirst)
        {
            Playersorder.Remove(P);
            Playersorder.Add(P);
        }
        StateChange(GameState.Round);
    }
    
   
}
