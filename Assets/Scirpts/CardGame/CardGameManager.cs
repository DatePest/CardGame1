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
    public bool IsSingleplayer =false;
    public PlayerDeckData[] playerdecks = new PlayerDeckData[2];
    public bool NeedWait = false;

    //public CardSpawnScript cardSpawnScript { get; private set; }
    //public SkillManager SkillSystem { get; private set; }
    public GameTurnSystem GameTurnSystem { get; private set; }
    public MapArea[] Maps = new MapArea[2];
    public GameTurnSystem_Net GameTurnSystem_Net { get; private set; }
    public  List<PlayerOBJ> Players  { get; private set; }
    public PlayerOBJ MyPlayer { get; private set; }
    public BattleUi BattleUI { get; private set; }
    public GameNotifyAction_Net GameNotifyAction_Net{ get; private set; }
    public TS_Manager Ts_Manager { get; private set; }
    public Finger_Guessing FInger_Guessing { get; private set; }
    public CardSpawnManager CardSpawnManager { get; private set; }
    public ulong UnitUID;
    public Dictionary<ulong, Unit> UnitDictionary =new();
    public int NextCardUid; // useed 
    public UIText SystemStateUIText , SendUI;
    public PlayerDeckDatas playerDeckDatas;
    private static CardGameManager instance;
    public static CardGameManager Instance
    {
        get { return instance; }
    }


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
        //cardSpawnScript = FindObjectOfType<CardSpawnScript>();
        //SkillSystem = FindObjectOfType<SkillManager>();
        Players = new();
        Ts_Manager = FindFirstObjectByType<TS_Manager>();
        FInger_Guessing = FindFirstObjectByType<Finger_Guessing>();
        CardSpawnManager = FindFirstObjectByType<CardSpawnManager>();
        BattleUI = FindFirstObjectByType<BattleUi>();
        GameNotifyAction_Net = GetComponent<GameNotifyAction_Net>();
        GameTurnSystem_Net = GetComponent<GameTurnSystem_Net>();
        GameTurnSystem = new(this);
        NetworkManager.Singleton.OnClientDisconnectCallback += PlayerDis;
        if (!NetworkManager.Singleton.IsHost) return;
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += LoadEventCompleted;
        playerDeckDatas = FindFirstObjectByType<PlayerDeckDatas>();
       
        //GameTurnSystem = new(this);
    }

    private void PlayerDis(ulong Id)
    {
      foreach(var a in Players)
        {
            if(a.OwnerClientId== Id)
            {
                //

                GameTurnSystem.StateChange(GameState.Finish);

                SendUI.SetTMPro(a.UserName.Value + " is Quit ");
            }
        }
    }

    private async void LoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (clientsCompleted.Count == NetworkManager.Singleton.ConnectedClientsList.Count)
        {
            await Task.Delay(1000);
            SeedDecks_ServerRpc();
            StateChange_ServerRpc(GameState.Initialization);
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
    [ClientRpc]
    void PlayerDeckData_ClientRpc(PlayerDeckData g)
    {
        if (IsServer) return;
        foreach(var a in g.Dards)
        {
            if (a == null) continue;
            Debug.Log(a.ToString());
        }
        foreach (var a in g.Heros)
        {
            if (a.UnitUid == null) continue;
            Debug.Log(a.UnitUid.ToString());
            Debug.Log(a.MapPoint.ToString());
        }

    }

    void Start()
    {
        NetworkSpawnServerRpc(NetworkManager.Singleton.LocalClientId ,SteamClient.Name);
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

    public void SceneQuit()
    {
        GameManager.Instance.SceneManager.LocalLoadSceneAsync(Scene.TestMain);
    }
    [ServerRpc(RequireOwnership = false)]
    public void NetworkSpawnServerRpc(ulong Id , string N )
    {
        //var T = NetworkManager.NetworkConfig.Prefabs.Prefabs.Where(n => n.Prefab.name == "BattlePlayerPrefab").SingleOrDefault();
        //T.UserName = $"Player  {Id}";
        var P = Instantiate(PlayerPrefab).GetComponent<PlayerOBJ>();
        P.GetComponent<NetworkObject>().SpawnAsPlayerObject(Id, true);
        P.UserName.Value = N;
        //T.GetComponent<NetworkObject>().DestroyWithScene = false;
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
    [ServerRpc]
    public void StateChange_ServerRpc(GameState g)
    {

        StateChange_ClientRpc(g);
    }
    [ClientRpc]
    void StateChange_ClientRpc(GameState g)
    {
        GameTurnSystem.StateChange(g);
    }
   

    public PlayerOBJ GetEnemyPlayer(ulong id)
    {
        foreach(var a in Players)
        {
            if (a.OwnerClientId != id)//NetworkManager.LocalClientId
            {
                return a;
            }
        }
        return null;
    }

    public MapArea GetUserEnemyMapArea(Unit Unit)
    {
        var id = Unit.UnitData.PlayerOwnerNumberID;
        for (int i=0; i< Maps.Length; i++)
        {
            if (Maps[i].PlayerOwnerID != id)
                return Maps[i];
        }
        return null;
    }
    public List<Unit> GetAllUnit()
    {
        var List = new List<Unit>();
        foreach (var a in Maps)
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
        foreach (var a in Maps)
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

    [ServerRpc]
    public void CreateUnit_ServerRpc(string Unit, UnitTpye unitTpye,int Owner,int MapID)
    {
        CreateUnit_ClientRpc(Unit, unitTpye, Owner, MapID, (ulong)UnitDictionary.Count);
        //n.GetComponent<NetworkBehaviour>().OnNetworkSpawn();
    }
    [ClientRpc]
    void CreateUnit_ClientRpc(string Unit , UnitTpye unitTpye, int Owner, int MapID, ulong Uid)
    {
        var U = Instantiate<Unit>(UnitPrefab);
        UnitDictionary.Add(Uid, U);
        var so = GameManager.Instance.DataBase.StringToUnit(Unit);
        U.Set_InitializationUnit(so, (ulong)Owner,Uid);
        U.UnitData.SetUnitTpye(unitTpye);
        if (Owner == 0)
        {
            var Map = Maps[0].MapAreas[MapID];
            Map.SetUnit(U);
        }
        else
        {
            var Map = Maps[1].MapAreas[MapID];
            Map.SetUnit(U);
        }
    }

 
    public void Hero_WinCheck(PlayerOBJ P)
    {
        var Check= true;
        

        for (int i= 0; i < 6; i++)
        {
            if (UnitDictionary[(ulong)i] == null || UnitDictionary[(ulong)i].UnitData.CurrentOwner != P) continue;
            if (UnitDictionary[(ulong)i].UnitData.IsDead != true)
                Check = false;
        }
        if (Check == true)
        {
            GameTurnSystem.StateChange(GameState.Finish);
           
            SendUI.SetTMPro(GetEnemyPlayer(P.OwnerClientId).UserName .Value+ " is Win");
            //WinUI.SetTMPro(P.NetworkObjectId + "__"+ P.OwnerClientId);
            return;
        }

        //////////B
        //Check = true;
        //for (int i = 3; i < 6; i++)
        //{
        //    if (UnitDictionary[(ulong)i].UnitData.IsDead != true)
        //        Check = false;
        //}
        //if (Check == true)
        //{
        //    GameTurnSystem.StateChange(GameState.Finish);
        //    return;
        //}
    }



}
