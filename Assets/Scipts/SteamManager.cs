using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Steamworks.Data;
using Unity.Netcode;

public class SteamManager : MonoBehaviour
{
    public Lobby? CurrentLobby { get; private set; }
    public async void StartSteamHost(int i)
    {
        CurrentLobby = await SteamMatchmaking.CreateLobbyAsync(i);
    }
    private void OnEnable()
    {
        SteamMatchmaking.OnLobbyCreated += LobbyCreated;
        SteamMatchmaking.OnLobbyEntered += LobbyEntered;
    }


    private void OnDisable()
    {
        SteamMatchmaking.OnLobbyCreated -= LobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= LobbyEntered;
        if (NetworkManager.Singleton == null) return;
        if (!NetworkManager.Singleton.IsHost) return;

        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnectCallback;
    }
    //public async void Client(ulong Id)
    //{
      
    //    Lobby[] lobbies = await SteamMatchmaking.LobbyList.WithSlotsAvailable(1).RequestAsync();
    //    Lobby lobby = new();
    //    foreach (Lobby L in lobbies)
    //    {
    //        if (L.Id == Id)
    //        {
    //            Debug.Log("Client Join");

    //            await L.Join();
    //            lobby = L;
    //            break;
    //        }
    //    }
    //}
    private void LobbyEntered(Lobby lobby)
    {
        CurrentLobby = lobby;
        //CurrentLobby = lobby;
        //Debug.Log("LobbyEntered + "+ lobby.Id.Value);
        //Debug.Log("Owner + " + lobby.Owner.Name);
        if (NetworkManager.Singleton.IsHost) return;
        Start_Client(lobby.Owner.Id);
    }
    
    private void LobbyCreated(Result result, Lobby lobby)
    {
        
        if (result == Result.OK)
        {
            lobby.SetPublic();
            lobby.SetJoinable(true);
            //lobby.SetGameServer(lobby.Owner.Id);
            //SteamMatchmaking.OnLobbyMemberLeave += HostLobbyMemberLeave;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
            NetworkManager.Singleton.StartHost();
            GameManager.Instance.SceneManager.EnableNetSceneManager();
            NetworkManager.Singleton.SceneManager.LoadScene(Scene.PvPLobby.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    

    void Start_Client(ulong Id)
    {
        Debug.Log(Id.ToString());
        GameManager.Instance.Transport.targetSteamId =  Id;
        NetworkManager.Singleton.OnClientConnectedCallback += ClientConnected; ;
        NetworkManager.Singleton.OnClientDisconnectCallback += ClientDisconnected;
        if(NetworkManager.Singleton.StartClient())
            Debug.Log("Start_Client!");
    }

    public void LobbyLeave()
    {
        if (CurrentLobby == null) return;
        Debug.Log("LobbyLeave");
        CurrentLobby?.Leave();
        CurrentLobby = null;
        GameManager.Instance.Transport.targetSteamId = 0;
    }

    
    private void ClientConnected(ulong clientId) => Debug.Log($"I'm connected, clientId={clientId}");

    private void ClientDisconnected(ulong clientId)
    {
        Debug.Log($"I'm disconnected, clientId={clientId}");

        NetworkManager.Singleton.OnClientDisconnectCallback -= ClientDisconnected;
        NetworkManager.Singleton.OnClientConnectedCallback -= ClientConnected;
        GameManager.Instance.Transport.targetSteamId = 0; 
    }

    private void OnClientConnectedCallback(ulong clientId) => Debug.Log($"Client connected, clientId={clientId}", this);

    private void OnClientDisconnectCallback(ulong clientId) => Debug.Log($"Client disconnected, clientId={clientId}", this);


}
