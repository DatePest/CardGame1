using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class Finger_Guessing : NetworkBehaviour
{
     NetworkList<Guessing_Data> Finger_Guessing_Data ;
    [SerializeField] PlayerFingerGuessing playerFinger;
    public PlayerFingerGuessing PlayerFinger => playerFinger;
    private void Awake()
    {
        Finger_Guessing_Data = new();
        playerFinger = Instantiate(playerFinger, transform);
        PlayerFinger.SetSeletOBJ();
    }
    public void  StartFinger_Guessing()
    {
        //Debug.Log("StartFinger_Guessing");
        StopAllCoroutines();
        StartCoroutine(Run());
    }

    
     IEnumerator Run()
    {
        Finger_Guessing_Data.Clear();
        StartFinger_GuessingClientRpc();
        while (Finger_Guessing_Data.Count < CardGameManager.Instance.Players.Count)
        {
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(CheckWin());
    }

    [ServerRpc(RequireOwnership = false)]
    public void ReturnPlayerFingerServerRpc(ulong playerid , int PlayerFinger)
    {
        Finger_Guessing_Data.Add(new (playerid, PlayerFinger));
       
    }
   
    IEnumerator CheckWin()
    {   // 1=™’“ 2 =Î“ª 3 = •z 
        //Debug.Log("CheckWin");
        Guessing_Data? T1 = null ,T2 = null;
        for (int N = 0; N < Finger_Guessing_Data.Count; N++)
        {
            if (Finger_Guessing_Data[N].ClientId == OwnerClientId)
                 T1 = Finger_Guessing_Data[N];
            else
                T2 = Finger_Guessing_Data[N];
        }
        NotifyCheckWinClientRpc();
        yield return new WaitForSeconds(3f);
        //Finger_GuessingNotify?.Invoke(Finger_Guessing_Data);
        if (T1.Value.Select == 1)
        {
                if (T2.Value.Select == 2)
                SelectOrderClientRpc(T2.Value.ClientId);
            else if (T2.Value.Select == 3)
                SelectOrderClientRpc(T1.Value.ClientId);
        }
        else if(T1.Value.Select == 2)
        {
                if (T2.Value.Select == 3)
                SelectOrderClientRpc(T2.Value.ClientId);
            else if (T2.Value.Select == 1)
                SelectOrderClientRpc(T1.Value.ClientId);
        }
        else if(T1.Value.Select == 3)
        {
                if (T2.Value.Select == 1)
                SelectOrderClientRpc(T2.Value.ClientId);
            else if (T2.Value.Select == 2)
                SelectOrderClientRpc(T1.Value.ClientId);
        }
        if(T1.Value.Select == T2.Value.Select)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(Run());
        }
    }
    [ClientRpc]
    void NotifyCheckWinClientRpc()
    {
        int Own = 0, En = 0;
        foreach (var a in Finger_Guessing_Data)
        {
            if (a.ClientId == NetworkManager.Singleton.LocalClientId)
            {
                Own = a.Select;
            }
            else
            {
                En = a.Select;
            }
        }
        PlayerFinger.FingerGuessingResult(Own, En);

    }
    [ClientRpc]
    void StartFinger_GuessingClientRpc()
    {
        PlayerFinger.FingerGuessingStart();
    }
    [ServerRpc(RequireOwnership = false)]
    public void SetPlayersOrderServerRpc( bool b , ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        SetPlayersOrderClientRpc(clientId, b);
    }
    [ClientRpc]
    void SetPlayersOrderClientRpc(ulong clientId, bool b)
    {
        CardGameManager.Instance.GameTurnSystem.SetPlayersOrder(clientId, b);
    }
    [ClientRpc]
    void SelectOrderClientRpc(ulong ID)
    {
        //Debug.Log("CheckSelectOrderClientRpcWin" + ID);
        if (ID == NetworkManager.Singleton.LocalClientId)
        {
            PlayerFinger.RunSelectOrder();
        }
       
    }

    public struct Guessing_Data : INetworkSerializable, IEquatable<Guessing_Data>
    {
        public ulong ClientId;
        public int Select;

        public Guessing_Data(ulong clientId, int select)
        {
            ClientId = clientId;
            Select = select;
        }

        public bool Equals(Guessing_Data other)
        {
            return ClientId == other.ClientId &&
                       Select== other.Select;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ClientId);
            serializer.SerializeValue(ref Select);
        }
    }
}
