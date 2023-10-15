using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[System.Serializable]
public class DeckData
{
    public HeroPointData[] Heros = new HeroPointData[3];
    public string[] Dards = new string[30];
}
[System.Serializable]
public struct HeroPointData : INetworkSerializable, IEquatable<HeroPointData>
{
    public NetworkString UnitUid;
    public int MapPoint;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref UnitUid);
        serializer.SerializeValue(ref MapPoint);
    }
    public bool Equals(HeroPointData other)
    {
        return UnitUid == other.UnitUid && MapPoint == other.MapPoint;
    }
}
public struct PlayerDeckData : INetworkSerializable, IEquatable<PlayerDeckData>
{
    public HeroPointData[] Heros;
    public NetworkString[] Dards;
    public ulong ClientID;
    //public byte Number_Player;

    public PlayerDeckData(DeckData deckData, ulong clientID )
    {
        Heros = deckData.Heros ;
        Dards = new NetworkString[deckData.Dards.Length];
        ClientID = clientID;
        //Number_Player = number_Player;
        SetSelectCardId(deckData.Dards);
    }
    public void SetSelectCardId(string[] List)
    {
        for (int i = 0; i < List.Length; i++)
        {
            if(List[i] !=null)
            Dards[i] = List[i];
        }
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Heros);
        serializer.SerializeValue(ref Dards);
        serializer.SerializeValue(ref ClientID);
        //serializer.SerializeValue(ref Number_Player);
    }
    public bool Equals(PlayerDeckData other)
    {
        return Heros == other.Heros && Dards == other.Dards ;
    }
}


