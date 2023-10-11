using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using System;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] RawImage isReady;
    //[SerializeField] bool IsReady=false;
    public bool IsReady;
    public LobbySceneManager lobbyManager;
    public ulong OwnerID;//{ get; private set; }
    public string PlayerName { get { return playerName.text; } }
  

    public void SetOwner(ulong Id, string Name)
    {
        //Debug.Log("SetOwner");
        gameObject.SetActive(true);
        OwnerID = Id;
        playerName.text = Name;
        IsReady = false;
    }
    public void LeaveOwnerSc()
    {
       // OwnerID = null;
        gameObject.SetActive(false);
        IsReady = false;
    }
    public void IsReadyUpdata() // Loacl
    {
        IsReady = !IsReady;
        lobbyManager.HideLoadDeckButton(IsReady);
        lobbyManager.lobbyState_.PlayerReady_ServerRpc(IsReady);
       
    }
    public void SetIsReady(bool b)
    {
        IsReady = b;
        ReadyColor();
        if (NetworkManager.Singleton.IsHost)
        {
            lobbyManager.CheckAllReady();
        }
    }
    void ReadyColor()
    {
        if (IsReady == true)
            isReady.color = Color.green;
        else
            isReady.color = Color.red;
    }
    

   

}
