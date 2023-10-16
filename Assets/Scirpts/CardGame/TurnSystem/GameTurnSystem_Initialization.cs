using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameTurnSystem_Initialization : I_StatePattern
{
    protected CardGameManager gameManagerInstance;
    public GameTurnSystem_Initialization(CardGameManager GameManagerInstance)
    {
        gameManagerInstance = GameManagerInstance;
    }
    public override void StateEnter()
    {
        if (!NetworkManager.Singleton.IsHost) return;
        var P1 = gameManagerInstance.Players[0].OwnerClientId;
        var P2 = gameManagerInstance.Players[1].OwnerClientId;
        gameManagerInstance.CardGame_Ctrl.SetPlayerOwnerNumberID_ClientRpc((byte)P1, 0);
        gameManagerInstance.CardGame_Ctrl.SetPlayerOwnerNumberID_ClientRpc((byte)P2, 1);
        gameManagerInstance.CardSpawnManager.SetCardSpawnScriptsOwnerID_ServerRpc(0, (byte)P1);
        gameManagerInstance.CardSpawnManager.SetCardSpawnScriptsOwnerID_ServerRpc(1, (byte)P2);

        gameManagerInstance.CardGame_Ctrl.Find_CardSpawnScriptClientRpc();
        gameManagerInstance.CardSpawnManager.InstantiateDeck_ServerRpc();
        gameManagerInstance.GameSceneUI.FInger_Guessing.StartFinger_Guessing();
        gameManagerInstance.CardSpawnManager.ClientSynchronizServerRpc((byte)P1, CardsPileEnum.deck,true);
        gameManagerInstance.CardSpawnManager.ClientSynchronizServerRpc((byte)P2, CardsPileEnum.deck, true);
        /// addUnit
        for (int A = 0; A < gameManagerInstance.playerDeckDatas.ListPlayerDeckDatas.Count; A++)
        {

            var T = gameManagerInstance.playerDeckDatas.ListPlayerDeckDatas[A];
            for (int i = 0; i < T.Heros.Length; i++)
            {
                CardGame_Ctrl_Net.Instance.CreateUnit_ServerRpc(T.Heros[i].UnitUid , UnitTpye.Hero, (int)T.ClientID, T.Heros[i].MapPoint);
            }
        }
    }
    
    public override void StateExit()
    {
       //gameManagerInstance.GameTurnSystem_Net.Start_CardSpawnScriptClientRpc(gameManagerInstance.GameTurnSystem.playersorder[0].OwnerClientId);
    }
    public override void StateUpdate()
    {
        //throw new System.NotImplementedException();
    }


}
