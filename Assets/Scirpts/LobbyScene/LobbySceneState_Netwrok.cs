//#define Test
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using Steamworks;
using Steamworks.Data;

public class LobbySceneState_Netwrok : NetworkBehaviour
{
     List<LobbyPlayerState> m_LobbyPlayers;
     LobbySceneManager lobbyManager;
    //public List<LobbyPlayerState> LobbyPlayers => m_LobbyPlayers;
    [SerializeField] PlayerDeckDatas playerDeckDatasObj;
    byte myPlayerNumber;
     void Awake()
    {
        lobbyManager = FindAnyObjectByType<LobbySceneManager>();
    }
    private void OnDisable()
    {
        SteamMatchmaking.OnLobbyMemberJoined -= LobbyMemberJoined;

        SteamMatchmaking.OnLobbyMemberLeave -= LobbyMemberLeave;

        var Net = NetworkManager.Singleton;
        //if (Net.IsServer)
        //{
        //    Net.OnClientConnectedCallback += ClientConnectedCallback;
        //    Net.OnClientDisconnectCallback += ClientDisconnectCallback;
        //}
        if (Net.IsHost)
        {
            SteamMatchmaking.OnLobbyMemberLeave -= HostLobbyMemberLeave;
            SteamMatchmaking.OnLobbyMemberJoined -= HostLobbyMemberJoined;
        }
        if (Net.IsClient)
        {
            //LobbyPlayers.OnListChanged -= LobbyListChanged;
        }
    }
    public void RStart()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            //GetComponent<NetworkObject>().Spawn();
            m_LobbyPlayers = new();
        }
        Debug.Log(SteamClient.Name + "__Clinet");
        var Net = NetworkManager.Singleton;
        if (Net.IsHost)
        {
            TestMemberJoined();//Test Only
            SteamMatchmaking.OnLobbyMemberLeave += HostLobbyMemberLeave;
            SteamMatchmaking.OnLobbyMemberJoined += HostLobbyMemberJoined;
        }
        if (Net.IsClient)
        {
            SteamMatchmaking.OnLobbyMemberJoined += LobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave += LobbyMemberLeave;
            updateLobby_ServerRpc(SteamClient.SteamId, SteamClient.Name);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    void updateLobby_ServerRpc(ulong Steamid, string Name, ServerRpcParams serverRpcParams = default)
    {
        LobbyPlayerState playerState = new(Steamid, serverRpcParams.Receive.SenderClientId, Name, (byte)m_LobbyPlayers.Count, false);
        m_LobbyPlayers.Add(playerState);
        LobbyListChanged();
    }
    private void HostLobbyMemberJoined(Lobby lobby, Friend SteamClient)
    {
        Debug.Log("HostLobbyMemberJoined");
    }
    private void HostLobbyMemberLeave(Lobby lobby, Friend Client)
    {
        Debug.Log("HostLobbyMemberLeave");
        for(int i=0;i< m_LobbyPlayers.Count; i++)
        {
            if (m_LobbyPlayers[i].ClientSteamId == Client.Id)
            {
                m_LobbyPlayers.Remove(m_LobbyPlayers[i]);
                break;
            }
        }
        LobbyListChanged();
    }

    [System.Diagnostics.Conditional("Test")]
    void TestMemberJoined()
    {
        NetworkManager.OnClientConnectedCallback += MemberJoined;//Test only
    }
    private void MemberJoined(ulong obj)
    {
        LobbyPlayerState playerState = new(0, obj, "Test Player", (byte)m_LobbyPlayers.Count, false);
        m_LobbyPlayers.Add(playerState);

        LobbyListChanged();
    }

    

    void NetSetLobbyPlayerState(ulong CilentId, LobbyPlayerState[] LPs)
    {
        if (NetworkManager.Singleton.LocalClientId != CilentId) return;
    }

    private void LobbyListChanged()
    {
        Debug.Log("LobbyListChanged" + m_LobbyPlayers.Count);
        for (int i = 0; i < lobbyManager.Playersolt.Length; i++)
        {
            if (m_LobbyPlayers.Count > i)
            {
                var a = m_LobbyPlayers[i];
                a.Number = (byte)i;
                m_LobbyPlayers[i] = a;
                //lobbyManager._Players[i].SetOwner(m_LobbyPlayers[i].ClientSteamId, m_LobbyPlayers[i].m_PlayerName);
                lobbySet_ClientRpc(i, a);
            }
            else
            {
                //lobbyManager._Players[i].LeaveOwnerSc();
                lobbyLeave_ClientRpc(i);
            }

        }
    }
    [ClientRpc]
    void lobbySet_ClientRpc(int i, LobbyPlayerState State)
    {
        myPlayerNumber = State.Number;
        lobbyManager.Playersolt[i].SetOwner(State.ClientId, State.m_PlayerName);
        if (State.Ready)
            lobbyManager.Playersolt[i].SetIsReady(State.Ready);
    }
    [ClientRpc]
    void lobbyLeave_ClientRpc(int i)
    {
        lobbyManager.Playersolt[i].LeaveOwnerSc();
    }

    
    [ServerRpc(RequireOwnership = false)]
    public void PlayerReady_ServerRpc(bool B , ServerRpcParams serverRpcParams = default)
    {
        int I=0;
        for (int i = 0; i < m_LobbyPlayers.Count; i++)
        {
            if (m_LobbyPlayers[i].ClientId == serverRpcParams.Receive.SenderClientId)
            {
                I = i;
                break;
            }
        }
        var T = m_LobbyPlayers[I];
        T.Ready = B;
        m_LobbyPlayers[I] = T;
        lobbyIsReady_ClientRpc(m_LobbyPlayers[I]);
    }
    [ClientRpc]
    void lobbyIsReady_ClientRpc(LobbyPlayerState State)
    {
        lobbyManager.Playersolt[State.Number].SetIsReady(State.Ready);
    }
    private void LobbyMemberLeave(Lobby lobby, Friend Client)
    {
        Debug.Log(Client.Name + "__Leave");
    }

    private void LobbyMemberJoined(Lobby lobby, Friend Client)
    {
        Debug.Log(Client.Name + "Joined");

    }

    private void ClientConnectedCallback(ulong Client)
    {
        Debug.Log("ClientConnectedCallback "+ Client.ToString());
    }

    private void ClientDisconnectCallback(ulong Client)
    {
        Debug.Log("ClientConnectedCallback " + Client.ToString());
    }
   
    
   
    [ServerRpc]
    public void GetDeckData_ServerRpc()
    {
        playerDeckDatasObj = Instantiate(playerDeckDatasObj, null);
        GetDeckData_ClientRpc();
    }
    [ClientRpc]
     void GetDeckData_ClientRpc()
    {
        //if (IsServer||IsHost) return;
        var data = lobbyManager.loadDeck.GetDeck();
        if(data != null)
        {
            SeedDeckData_ServerRpc(new PlayerDeckData(data , NetworkManager.LocalClientId, myPlayerNumber)  );
        }
       
    }
    [ServerRpc(RequireOwnership = false)]
    public void SeedDeckData_ServerRpc(PlayerDeckData deckData, ServerRpcParams serverRpcParams = default)
    {
       var  ClientId = serverRpcParams.Receive.SenderClientId;
        foreach (var a in m_LobbyPlayers)
        {
            if(a.ClientId == ClientId)
            {
                playerDeckDatasObj.SetPlayerDeckDatas(deckData, ClientId ,a.Number);
                break;
            }
        }
       
        if(playerDeckDatasObj.Dictionary_ClientID_PlayerDeckDatas.Count == m_LobbyPlayers.Count)
        {
            GameManager.Instance.SceneManager.NetLoadSceneAsync(Scene.PvPScene);
        }
    }

    

}
public struct LobbyPlayerState : INetworkSerializable, IEquatable<LobbyPlayerState>
{
    public ulong ClientSteamId;
    public ulong ClientId;
    public NetworkString m_PlayerName;
    public bool Ready;
    public byte Number;
    public LobbyPlayerState(ulong clientSteamId, ulong clientId, string name, byte number, bool ready = false)
    {
        ClientSteamId = clientSteamId;
        ClientId = clientId;
        Ready = ready;
        Number = number;
        m_PlayerName = new(name);
    }

    public bool Equals(LobbyPlayerState other)
    {
        return ClientSteamId == other.ClientSteamId &&
                   m_PlayerName.Equals(other.m_PlayerName) &&
                   ClientId == other.ClientId&&
                   Ready == other.Ready;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
       
        serializer.SerializeValue(ref ClientSteamId);
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref m_PlayerName);
        serializer.SerializeValue(ref Ready);
        serializer.SerializeValue(ref Number);
    }
}