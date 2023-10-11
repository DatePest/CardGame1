using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using Steamworks;
using Steamworks.Data;

public class LobbySceneManager : MonoBehaviour
{
    [SerializeField] GameObject LoadButton;
    [SerializeField] PlayerCard[]  playersolt = new PlayerCard[2];
    [SerializeField] Button StartButton;
    [SerializeField] TextMeshProUGUI LobbyID;
    public RoomLoadDeck loadDeck;
    public LobbySceneState_Netwrok lobbyState_;
    public PlayerCard[] _Players => playersolt;
    bool Startlook = false;
    // Start is called before the first frame update
    private void Awake()
    {
        StartButton.gameObject.SetActive(false);
        lobbyState_ = FindAnyObjectByType<LobbySceneState_Netwrok>();
        loadDeck = FindAnyObjectByType<RoomLoadDeck>();
        playersolt = FindObjectsByType<PlayerCard>(0);
        //lobbyState_ = FindAnyObjectByType<LobbyState_Netwrok>();
    }
    void Start()
    {
        LobbyID.text = "ID = " +GameManager.Instance.steamManager.CurrentLobby.Value.Id;
        foreach (var a in playersolt)
        {
            a.lobbyManager = this;
            a.gameObject.SetActive(false);
        }
        IsHost();
        lobbyState_.RStart();
    }

    void IsHost()
    {
        if (!NetworkManager.Singleton.IsHost) return;
        //StartButton.gameObject.SetActive(true);
    }
    void IsClinet()
    {
        if (!NetworkManager.Singleton.IsClient) return;
    }
    public void CopyLobbyIDButton()
    {
        TextEditor text = new TextEditor();
        text.text = GameManager.Instance.steamManager.CurrentLobby.Value.Id.ToString();
        text.OnFocus();
        text.Copy();
    }

    public void GameStartButton()
    {
       
        if (Startlook) return;
        Startlook = true;
        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
        {
            Debug.Log("GameStartButton");
            
            lobbyState_.GetDeckData_ServerRpc();
        }
    }

    public void ReadyButton()
    {
        if (!loadDeck.DeckCheck) return;
        foreach (var a in playersolt)
        {
            if (a.OwnerID == NetworkManager.Singleton.LocalClientId)
            {
                a.IsReadyUpdata();
                return;
            }
        }
    }
    public void LobbyLeaveButton()
    {
        GameManager.Instance.steamManager.LobbyLeave();
        GameManager.Instance.Transport.Shutdown();
        NetworkManager.Singleton.Shutdown();
        GameManager.Instance.SceneManager.LocalLoadSceneAsync(Scene.TestMain);
    }
    public void CheckAllReady()
    {
        foreach (var a in playersolt)
        {
            if (a.IsReady == false)
            {
                StartButton.gameObject.SetActive(false);
                return;
            }
                
        }
        StartButton.gameObject.SetActive(true);
    }
    public void HideLoadDeckButton(bool b)
    {
        if(b)
            LoadButton.gameObject.SetActive(false);
        else
            LoadButton.gameObject.SetActive(true);
    }
}
