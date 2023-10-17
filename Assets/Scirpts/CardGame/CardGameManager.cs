using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class CardGameManager : NetworkBehaviour
{
    [SerializeField]
    PlayerOBJ PlayerPrefab;
    [SerializeField]
    Unit UnitPrefab;
    public Unit _UnitPrefab => UnitPrefab;
    public bool IsSingleplayer { get; private set; } = false;
    public PlayerDeckData[] playerdecks = new PlayerDeckData[2];
    public bool NeedWait = false;
    public Dictionary<ulong, Unit> UnitDictionary = new();
    public MapArea[] Maps = new MapArea[2];
    public GameSceneUI GameSceneUI { get; private set; }
    public  List<PlayerOBJ> Players  { get; private set; }
    public PlayerOBJ MyPlayer { get; private set; }
    public GameTurnSystem GameTurnSystem { get; private set; }
    public GameNotifyAction_Net GameNotifyAction_Net{ get; private set; }
    public TS_Manager Ts_Manager { get; private set; }
   
    public CardSpawnManager CardSpawnManager { get; private set; }
   
    public PlayerDeckDatas playerDeckDatas { get; private set; }
    private static CardGameManager instance;
    public static CardGameManager Instance => instance;
    private  void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Get a second instance of this classÅF" + this.GetType());
        }
        Players = new();
        Ts_Manager = FindFirstObjectByType<TS_Manager>();
        GameSceneUI = FindFirstObjectByType<GameSceneUI>();
        CardSpawnManager = FindFirstObjectByType<CardSpawnManager>();
        GameNotifyAction_Net = GetComponent<GameNotifyAction_Net>();
        GameTurnSystem = new(this);
        
        
        if (!NetworkManager.Singleton.IsHost) return;
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += LoadEventCompleted;
        playerDeckDatas = FindFirstObjectByType<PlayerDeckDatas>();
       
    }
    void Start()
    {
        NetworkSpawnServerRpc(NetworkManager.Singleton.LocalClientId, SteamClient.Name);
        //var T = FindObjectsOfType<MapSolt>();
        if (!IsServer) return;
    }
    private void OnDestroy()
    {
        if (playerDeckDatas != null)
            Destroy(playerDeckDatas.gameObject);
        instance = null;
        // OnNetworkDespawn();
        base.OnDestroy();
    }
    private async void LoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (clientsCompleted.Count == NetworkManager.Singleton.ConnectedClientsList.Count)
        {
            await Task.Delay(1000);
            SeedDecks_ServerRpc();
            CardGame_Ctrl_Net.Instance.StateChange_ServerRpc(GameState.Initialization);
        }
    }
    [ServerRpc]
    void SeedDecks_ServerRpc()
    {
       for(int i=0; i< playerDeckDatas.Dictionary_Number_PlayerDeckDatas.Count; i++)
        {
            SeedDecks_ClientRpc(playerDeckDatas.Dictionary_Number_PlayerDeckDatas[(byte)i],i);
        }
    }
    [ClientRpc]
    void SeedDecks_ClientRpc(PlayerDeckData data ,int i)
    {
        playerdecks[i] = data;
    }

    [ServerRpc(RequireOwnership = false)]
    public void NetworkSpawnServerRpc(ulong Id , string N )
    {
        var P = Instantiate(PlayerPrefab).GetComponent<PlayerOBJ>();
        P.GetComponent<NetworkObject>().SpawnAsPlayerObject(Id, true);
        P.UserName.Value = N;
        PlayerOBJsAdd_ClientRpc(P);
    }
    [ClientRpc]
    void PlayerOBJsAdd_ClientRpc(NetworkBehaviourReference Reference )
    {
        Reference.TryGet(out var T);
        if(T is PlayerOBJ)
        Players.Add((PlayerOBJ)T);
    }
    public void SetmyPlayer(PlayerOBJ P)
    {
        MyPlayer = P;
    }
   
}
