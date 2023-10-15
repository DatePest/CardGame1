using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System;
[System.Serializable]
public struct NetworkString : INetworkSerializable, IEquatable<NetworkString>
{
	private FixedString64Bytes info;

    public bool Equals(NetworkString other)
    {
		return info.Equals(other.info);
	}
	public NetworkString(string s)
    {
		info = s;
	}

	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		serializer.SerializeValue(ref info);
	}
	public override string ToString()
	{
		return info.ToString();
	}
	public static implicit operator string(NetworkString s) => s.ToString();
	public static implicit operator NetworkString(string s) => new NetworkString() { info = new FixedString64Bytes(s) };
	public static NetworkString[] GetArray(List<string> List)
    {
		NetworkString[] NewList = new NetworkString[List.Count];
		for (int a = 0; a < NewList.Length; a++)
		{
			NewList[a] = List[a];
		}
		return NewList;
	}
}