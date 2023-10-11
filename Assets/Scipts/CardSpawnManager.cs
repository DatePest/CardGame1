using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class CardSpawnManager : NetworkBehaviour
{
    //public List<string>[] PlayerDeck, PlayerHand, PlayerDisDeck, PlayerRemoveDeck;
    //byte? ObserverID;
    [SerializeField]
    [Header("[0]=P1,[1]=P2")]
    CardSpawnScript[] cardSpawnScripts = new CardSpawnScript[2];
    [SerializeField]
    Transform Children;
    public CardSpawnScript[] CardSpawnScripts { get => cardSpawnScripts; }
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
        for (byte i = 0; i < CardGameManager.Instance.playerdecks.Length; i++)
        {
            CardSpawnScripts[i].OwnerID = i;
        }

            //ObserverID = (byte)NetworkManager.Singleton.LocalClientId;

        if (!IsServer) return;
        //InstantiateDeck();
        
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
                CardSpawnScripts[i].InstantiateGoto(CardsPileEnum.deck, A, C);
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
    public void ClientSynchronizServerRpc(int cardSpawnScriptID, CardsPileEnum Target, bool RunShuffle =false, ServerRpcParams serverRpcParams = default)
    {
        //var clientId = serverRpcParams.Receive.SenderClientId;
        if(RunShuffle) cardSpawnScripts[cardSpawnScriptID].Shuffle_Cards(Target);
        var List = cardSpawnScripts[cardSpawnScriptID].FindDeckCardSoltList(Target);
        int[] CardUIDs= new int[List.Count];
        for(int i=0; i< CardUIDs.Length; i++)
        {
            CardUIDs[i] = List[i].CardUid;

        }
        SynchronizDeckClientRpc(cardSpawnScriptID, CardUIDs, Target);
    }
    [ClientRpc]
    void SynchronizDeckClientRpc(int cardSpawnScriptID, int[] CardUIDs, CardsPileEnum cardsPile)
    {
        cardSpawnScripts[cardSpawnScriptID].SynchronizeCurrentDeck(cardsPile , CardUIDs);
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
        
        var Script = CardGameManager.Instance.CardSpawnManager.CardSpawnScripts[(int)clientId];
        Script.FindCardGoto(CardId, From, Goto);
    }

    





}

