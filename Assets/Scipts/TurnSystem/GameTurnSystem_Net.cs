using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameTurnSystem_Net : NetworkBehaviour
{
    //int BattleReturn ,PlayersNumber;
   [ClientRpc]
   public void Start_CardSpawnScriptClientRpc(ulong FirstClientID)
    {
        for(int i=0; i < CardGameManager.Instance.CardSpawnManager.CardSpawnScripts.Length; i++)
        {
            if (i == (int)FirstClientID)
            {
                CardGameManager.Instance.CardSpawnManager.CardSpawnScripts[i].SpawnSizeFromToTarget(5, CardsPileEnum.deck, CardsPileEnum.hand);
            }
            else
            {
                CardGameManager.Instance.CardSpawnManager.CardSpawnScripts[i].SpawnSizeFromToTarget(6, CardsPileEnum.deck, CardsPileEnum.hand);
            }
        }
    }

    [ClientRpc]
    public void Find_CardSpawnScriptClientRpc()
    {
        foreach (var a in FindObjectsByType<PlayerOBJ>(FindObjectsSortMode.None))
        {
            a.CardSpawnScript_FindSetLoacl();
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void GameStateUpdateServerRpc()
    {
        GameStateUpdateClientRpc();
    }
    [ClientRpc]
    void GameStateUpdateClientRpc()
    {
        CardGameManager.Instance.GameTurnSystem.GameStateUpdate();
    }
    [ClientRpc]
    public void EndButtonClientRpc(ulong FirstClientID , bool b)
    {
        if (NetworkManager.Singleton.LocalClientId == FirstClientID)
            CardGameManager.Instance.MyPlayer.PlayerTrigger.SetActiveEndButton(b);
    }
    //[ServerRpc(RequireOwnership = false)]
    //public void BattleEnd_ServerRpc()
    //{
    //    BattleReturn++;
    //    if (BattleReturn == PlayersNumber)
    //        GameStateUpdateServerRpc();
    //}
    //public void BattleEndWait(int i)
    //{
    //    PlayersNumber = i;
    //    BattleReturn = 0;
    //}
    

}
