using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckDatas : MonoBehaviour
{
    public  List<PlayerDeckData> ListPlayerDeckDatas =new();
    public Dictionary<ulong, PlayerDeckData> Dictionary_ClientID_PlayerDeckDatas =new();
    public Dictionary<byte, PlayerDeckData> Dictionary_Number_PlayerDeckDatas =new();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetPlayerDeckDatas(PlayerDeckData data , ulong ClinetID ,byte i)
    {
        ListPlayerDeckDatas.Add(data);
        Dictionary_ClientID_PlayerDeckDatas.Add(ClinetID, data);
        Dictionary_Number_PlayerDeckDatas.Add(i, data);

    }

   
}
