using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Round_Battle : Round_IState
{

    public List<AttatkBase> Battle_Actions { get; private set; } = new();
    public  Action<AttatkBase> BattleActions_Event;
    public Action BattleActions_ClearEvent;
    BattleUi Ui;
    public Round_Battle(Round_IState nextState) : base(nextState)
    {
    }
    public override Round_State_Enum GetRound_State_Enum()
    {
        return Round_State_Enum.Round_Battle;
    }
    public override void StateEnter()
    {
        Round_ActionInvoke();
        if (Ui == null)
            Ui = CardGameManager.Instance.GameSceneUI.BattleUI;
        CurrentPlayer = null;
        if (NetworkManager.Singleton.IsHost)
        {
            // CardGameManager.Instance.GameTurnSystem_Net.BattleEndWait(CardGameManager.Instance.playerdecks.Length)
            //Debug.Log("BattleActions.Count = " + BattleActions.Count);
            if (Battle_Actions.Count > 0)
            for(int i=0; i< Battle_Actions.Count; i++)
                {
                    Ui.CurrnetAtk(i);
                    Battle_Actions[i].AtkAction();
                }
            CardGame_Ctrl_Net.Instance.GameStateUpdateServerRpc();
        }
        //CardGameManager.Instance.GameTurnSystem_Net.BattleEnd_ServerRpc();

    }
    public void BattleActionsAdd(AttatkBase attatk)
    {
        Battle_Actions.Add(attatk);
        BattleActions_Event?.Invoke(attatk);
    }
    public override void StateExit()
    {
        Battle_Actions.Clear();
        BattleActions_ClearEvent?.Invoke();
    }


    public override void StateUpdate()
    {
    }
}
