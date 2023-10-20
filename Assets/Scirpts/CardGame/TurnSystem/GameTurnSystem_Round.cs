using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameTurnSystem_Round : I_StatePattern
{
    protected CardGameManager gameManagerInstance;
    public GameTurnSystem_Round(CardGameManager GameManagerInstance)
    {
        gameManagerInstance = GameManagerInstance;
        CurrentRound_State = RoundInitialization();
    }

    PlayerOBJ Currentplayer;
    public List<PlayerOBJ> Players => gameManagerInstance.GameTurnSystem.Playersorder;
    int CurrenRound;
    public int Curren_Round => CurrenRound;

    public Dictionary<string, Round_IState> Round_IStates { get; private set; }
    public Round_IState CurrentRound_State { get; private set; }
    Round_IState Round_Start()
    {
        if (Round_IStates != null) return Round_IStates[nameof(Round_Start)];
        return RoundInitialization();
    }
    public override void StateEnter()
    { 
        Currentplayer = Players[0];
        CurrenRound += 1;
        //if (CurrenRound == 1) Players[1].cardSpawnScript.SpawnDeckToHand(1); // 1 round DefPlayer +1 Card;
        // Debug.Log($"CurrenRound__{CurrenRound}");
        CurrentRound_State = Round_Start();

        CurrentStateEnter();
    }
    public override void StateExit()
    {
        CardGame_PlayerUIManager.Instance.SetActiveEndButton(false);
        foreach (var a in Players)
        {
            
            a.SetCurrentReset();
            a.IsCanSpawn = false;
        }
        //Players.Remove(Currentplayer);
        //Players.Add(Currentplayer);
        //StateEnter();
    }

    public override void StateUpdate()
    {
        CurrentRound_State.StateExit();
        if (CurrentRound_State.NextState != null)
        {
            CurrentRound_State = CurrentRound_State.NextState;
            CurrentStateEnter();
        }
        else {
            Players.Remove(Currentplayer);
            Players.Add(Currentplayer);
            StateEnter();
        }
    }


    void CurrentStateEnter()
    {
        CardGameManager.Instance.GameSceneUI.GameStateUIText.SetTMPro(CurrentRound_State.GetType().Name);
        CurrentRound_State.StateEnter();
       // Debug.Log("" + CurrentRound_State.GetType().Name);
        
    }

    Round_IState RoundInitialization()
    {
        Round_IState State =    new Round_Start(
                                new Round_AttackR1(
                                new Round_DefR1(
                                new Round_AttackR2(
                                new Round_Battle(
                                new Round_EndR1(
                                new Round_EndR2(
                                new Round_End_Event(
                                    null))))))));
        
        var S = State;
        Round_IStates = new();
        while (S != null)
        {
           // Debug.Log("DictionaryADD+" + S.GetType().Name);
            Round_IStates.Add(S.GetType().Name, S);
            S.gameTurnSystem_Round = this;
            if (S.NextState ==null)
                break;
            if(S is Round_Battle)
            {
                //if (gameManagerInstance == null) Debug.LogError("1");
                //if (gameManagerInstance.GameSceneUI == null) Debug.LogError("2");
                //if (gameManagerInstance.GameSceneUI.BattleUI == null) Debug.LogError("3");
                gameManagerInstance.GameSceneUI.BattleUI.BattleShowScreen(ref S);
            }
            S = S.NextState;
        }
        //Debug.Log("Round_IStates   +" + Round_IStates.Count);
        
        return State;
    }

}

public enum Round_State_Enum
{
    Round_Start, Round_AttackR1, Round_DefR1, Round_AttackR2, Round_Battle, Round_EndR1, Round_EndR2, Round_End_Event
}