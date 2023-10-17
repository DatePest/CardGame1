using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CardGame_Ctrl_Net : NetworkBehaviour
{
    CardGameManager cardGameManager;
    public static CardGame_Ctrl_Net Instance { get; private set;}

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("CardGame_Ctrl Instance != null ");
            Destroy(this);
        }
        NetworkManager.Singleton.OnClientDisconnectCallback += PlayerClientDisconnect;
        cardGameManager = GetComponent<CardGameManager>();
    }

    private void PlayerClientDisconnect(ulong Id)
    {
        foreach (var a in cardGameManager.Players)
        {
            if (a.OwnerClientId == Id)
            {
                //

                cardGameManager.GameTurnSystem.StateChange(GameState.Finish);

                cardGameManager.GameSceneUI.SendUI.SetTMPro(a.UserName.Value + " is Quit ");
            }
        }
    }
    #region Inst
    [ClientRpc]
    public void SetPlayerOwnerNumberID_ClientRpc(byte ID, int Number)
    {
        cardGameManager.Maps[Number].SetPlayerOwnerID(ID);
    }
    [ClientRpc]
    public void Find_CardSpawnScriptClientRpc()
    {
        foreach (var a in cardGameManager.Players)
        {
            a.CardSpawnScript_FindSetLoacl();
        }
    }
    #endregion

    #region GameStateUpdate
    [ServerRpc]
    public void GameStateUpdateServerRpc() => GameStateUpdateClientRpc();
    [ClientRpc]
    void GameStateUpdateClientRpc() => cardGameManager.GameTurnSystem.GameStateUpdate();
    #endregion
    //[ClientRpc]
    //public void EndButtonClientRpc(ulong FirstClientID , bool b)
    //{
    //    if (NetworkManager.Singleton.LocalClientId == FirstClientID)
    //        CardGameManager.Instance.MyPlayer.PlayerTrigger.SetActiveEndButton(b);
    //}
    #region GameStateChange
    [ServerRpc(RequireOwnership = false)]
    public void StateChange_ServerRpc(GameState g)=> StateChange_ClientRpc(g);
    [ClientRpc]
    void StateChange_ClientRpc(GameState g)=> cardGameManager.GameTurnSystem.StateChange(g);
    #endregion

    #region WinCheck
    public void Hero_WinCheck(PlayerOBJ P)
    {
        var Check = true;


        for (int i = 0; i < 6; i++)
        {
            if (cardGameManager.UnitDictionary[(ulong)i] == null || cardGameManager.UnitDictionary[(ulong)i].UnitData.CurrentOwner != P) continue;
            if (cardGameManager.UnitDictionary[(ulong)i].UnitData.IsDead != true)
                Check = false;
        }
        if (Check == true)
        {
            cardGameManager.GameTurnSystem.StateChange(GameState.Finish);

            cardGameManager.GameSceneUI.SendUI.SetTMPro(GetEnemyPlayer(P.OwnerClientId).UserName.Value + " is Win");
            return;
        }

    }
    #endregion
    #region GetPlayer/Unit/Map
    public PlayerOBJ GetEnemyPlayer(ulong id)
    {
        foreach (var a in cardGameManager.Players)
        {
            if (a.OwnerClientId != id)
            {
                return a;
            }
        }
        return null;
    }
    public MapArea GetUserEnemyMapArea(Unit Unit)
    {
        var id = Unit.UnitData.PlayerOwnerNumberID;
        for (int i = 0; i < cardGameManager.Maps.Length; i++)
        {
            if (cardGameManager.Maps[i].PlayerOwnerID != id)
                return cardGameManager.Maps[i];
        }
        return null;
    }
    public List<Unit> GetAllUnit()
    {
        var List = new List<Unit>();
        foreach (var a in cardGameManager.Maps)
        {
            var u = a.GetAllUnit();
            if (u != null)
            {
                foreach (var Unit in u)
                {
                    List.Add(Unit);
                }
            }
        }
        return List;
    }
    public List<MapSolt> GetAllMapSolt()
    {
        var List = new List<MapSolt>();
        foreach (var a in cardGameManager.Maps)
        {
            var u = a.GetAllMapSolt();
            if (u != null)
            {
                foreach (var Unit in u)
                {
                    List.Add(Unit);
                }
            }
        }
        return List;
    }
    public List<Unit> GetAllSelectRange(AbilityNeedData data, TargetRange target)
    {
        return GetAllSelectRange(data.CurrentUsePlayer, target);
    }
    public List<Unit> GetAllSelectRange(PlayerOBJ player, TargetRange target)
    {
        var UList = GetAllUnit();
        List<Unit> N = new();
        switch (target)
        {
            case TargetRange.All:
                return UList;
            case TargetRange.Enemy:
                foreach (var u in UList)
                {
                    if (u.UnitData.PlayerOwnerNumberID != player.OwnerClientId)
                        N.Add(u);
                }
                return N;
            case TargetRange.Own:
                foreach (var u in UList)
                {
                    if (u.UnitData.PlayerOwnerNumberID == player.OwnerClientId)
                        N.Add(u);
                }
                return N;
        }
        return null;
    }
    #endregion
    #region CreateUnit
    [ServerRpc]
    public void CreateUnit_ServerRpc(string Unit, UnitTpye unitTpye, int Owner, int MapID)
                => CreateUnit_ClientRpc(Unit, unitTpye, Owner, MapID, (ulong)cardGameManager.UnitDictionary.Count);
    [ClientRpc]
    void CreateUnit_ClientRpc(string Unit, UnitTpye unitTpye, int Owner, int MapID, ulong Uid)
    {
        var U = Instantiate<Unit>(cardGameManager._UnitPrefab);
        cardGameManager.UnitDictionary.Add(Uid, U);
        var so = GameManager.Instance.DataBase.StringToUnit(Unit);
        U.Set_InitializationUnit(so, (ulong)Owner, Uid);
        U.UnitData.SetUnitTpye(unitTpye);
        if (Owner == 0)
        {
            var Map = cardGameManager.Maps[0].MapAreas[MapID];
            Map.SetUnit(U);
        }
        else
        {
            var Map = cardGameManager.Maps[1].MapAreas[MapID];
            Map.SetUnit(U);
        }
    }
    #endregion
    #region SceneQuit
    public void SceneQuit()
    {
        GameManager.Instance.SceneManager.LocalLoadSceneAsync(Scene.TestMain);
    }
    #endregion
}
