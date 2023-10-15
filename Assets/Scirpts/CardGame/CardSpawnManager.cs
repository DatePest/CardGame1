using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CardSpawnManager : NetworkBehaviour
{
    //public List<string>[] PlayerDeck, PlayerHand, PlayerDisDeck, PlayerRemoveDeck;
    //byte? ObserverID;
    [SerializeField]
    [Header("[0]=P1,[1]=P2")]
    CardSpawnScript[] cardSpawnScripts = new CardSpawnScript[2];
    [SerializeField]
    Transform Children;
    //public CardSpawnScript[] CardSpawnScripts { get => cardSpawnScripts; }

    public Dictionary<byte, CardSpawnScript> Dictionary_CardSpawnScripts { get; private set; } = new();
    private void Awake()
    {
        //PlayerDeck = new List<string>[2];
        //PlayerHand = new List<string>[2];
        //PlayerDisDeck = new List<string>[2];
        //PlayerRemoveDeck = new List<string>[2];
    }
    private void Start()
    {

        if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer)
        {
            Children.localRotation = Quaternion.Euler(180, 180, 0);

        }
      
    }
    

    [ServerRpc(RequireOwnership = false)]
    public void SetCardSpawnScriptsOwnerID_ServerRpc(int i, byte clientid)
    {
        SetCardSpawnScriptsOwnerID_ClientRpc(i, clientid);
    }
    [ClientRpc]
    public void SetCardSpawnScriptsOwnerID_ClientRpc(int i ,byte clientid)
    {
        Dictionary_CardSpawnScripts.Add(clientid, cardSpawnScripts[i]);
        Dictionary_CardSpawnScripts[clientid].OwnerID = clientid;
    }

    [ServerRpc(RequireOwnership = false)]
    public void InstantiateDeck_ServerRpc()
    {
        InstantiateDeck_ClientRpc();
    }
    [ClientRpc]
    void InstantiateDeck_ClientRpc() => InstantiateDeck();
    public void InstantiateDeck()
    {
        int A = 0;
        for (int i = 0; i < CardGameManager.Instance.playerdecks.Length; i++)
        {
            var deck = CardGameManager.Instance.playerdecks[i].Dards;
            foreach (var C in deck)
            {
                cardSpawnScripts[i].InstantiateGoto(CardsPileEnum.deck, A, C);
                A++;
            }
        }
        
    }
        //[ServerRpc]//之後重新做成全部同步
        //public void DeckShuffleAndSynchronizeServerRpc(CardsPileEnum cardsPile)
        //{
        //    for (int i = 0; i < cardSpawnScripts.Length; i++)
        //    {
        //        cardSpawnScripts[i].Shuffle_Cards(cardsPile);
        //    }
        //    for (int i = 0; i < cardSpawnScripts.Length; i++)
        //    {
        //        var List = cardSpawnScripts[i].FindDeckStringList(cardsPile);
        //        var T = NetworkString.GetArray(List);
        //        SynchronizDeckClientRpc(i, T, cardsPile);
        //    }
        //}
        [ServerRpc(RequireOwnership = false)]
    public void ClientSynchronizServerRpc(byte OwnerID, CardsPileEnum Target, bool RunShuffle =false, ServerRpcParams serverRpcParams = default)
    {
        //var clientId = serverRpcParams.Receive.SenderClientId;
        if(RunShuffle) Dictionary_CardSpawnScripts[OwnerID].Shuffle_Cards(Target);
        var List = Dictionary_CardSpawnScripts[OwnerID].FindDeckCardSoltList(Target);
        int[] CardUIDs= new int[List.Count];
        for(int i=0; i< CardUIDs.Length; i++)
        {
            CardUIDs[i] = List[i].CardUid;

        }
        SynchronizDeckClientRpc(OwnerID, CardUIDs, Target);
    }
    [ClientRpc]
    void SynchronizDeckClientRpc(byte OwnerID, int[] CardUIDs, CardsPileEnum cardsPile)
    {
        Dictionary_CardSpawnScripts[OwnerID].SynchronizeCurrentDeck(cardsPile , CardUIDs);
    }

  

    [ServerRpc(RequireOwnership = false)]
    public void SpawnHandToDisdeckServerRpc(int CardId, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        SpawnFindGotoClientRpc(CardId, clientId, CardsPileEnum.deck, CardsPileEnum.hand);
    }
    [ClientRpc]
    void SpawnFindGotoClientRpc(int CardId, ulong clientId, CardsPileEnum From, CardsPileEnum Goto)
    {
        if (NetworkManager.LocalClientId == clientId) return;
        
        var Script = CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId];
        Script.FindCardGoto(CardId, From, Goto);
    }

    





}

