using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Steamworks;
using Steamworks.Data;
using System;
using Unity.Netcode;

public class SteamMain : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject Req;
    bool UseIng =false;
    private void Start()
    {
        GameManager.Instance.Transport.Shutdown();
        NetworkManager.Singleton.Shutdown();
    }

    public async void Host()
    {
        //Debug.Log("Host");
        GameManager.Instance.steamManager.StartSteamHost(2);

    }
    public async void Client()
    {
        if (UseIng) return;
        ulong Id;
        if (!ulong.TryParse(inputField.text, out Id)) return;
        UseIng = true;
        inputField.gameObject.transform.parent.gameObject.SetActive(false);
        //NetworkManager.Singleton.OnTransportFailure += LobbyFailure;
        
       
        Lobby[] lobbies = await SteamMatchmaking.LobbyList.WithSlotsAvailable(1).RequestAsync();
        Lobby lobby = new();
        foreach (Lobby L in lobbies)
        {
            if(L.Id == Id)
            {
                await L.Join();
                lobby = L;
                return;
            }
        }
        Debug.Log("Null");
        Req.SetActive(true);
        UseIng = false;
        //NetworkManager.Singleton.OnTransportFailure -= LobbyFailure;
    }

    private void LobbyFailure()
    {
        UseIng = false;
        Req.SetActive(true);
        NetworkManager.Singleton.OnTransportFailure -= LobbyFailure; 
    }
}
