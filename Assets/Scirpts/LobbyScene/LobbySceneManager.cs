using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using Steamworks;
using Steamworks.Data;
namespace LobbyScene
{
    public class LobbySceneManager : MonoBehaviour
    {
        //[SerializeField] GameObject LoadButton;
        LobbyScene_Ctrl lobbyScene_Ctrl;
        public LobbyButton lobbyButton { get; private set; }
        public LobbyLoadDeck lobbyLoadDeck { get; private set; }
        public LobbySceneState_Netwrok lobbyState_ { get; private set; }
        public LobbyPlayer[] Playersolt { get; private set; } = new LobbyPlayer[2];
        //[SerializeField] Button StartButton;
        [SerializeField] TextMeshProUGUI LobbyID;
       
        
        private void Awake()
        {
            lobbyScene_Ctrl = new(this);
            lobbyButton = FindAnyObjectByType<LobbyButton>();
            lobbyLoadDeck = FindAnyObjectByType<LobbyLoadDeck>();
            lobbyState_ = FindAnyObjectByType<LobbySceneState_Netwrok>();
            Playersolt = FindObjectsByType<LobbyPlayer>(0);
        }
        void Start()
        {
            LobbyID.text = "ID = " + GameManager.Instance.steamManager.CurrentLobby.Value.Id;
            foreach (var a in Playersolt)
            {
                a.gameObject.SetActive(false);
            }
            IsHost();
            lobbyState_.RStart();
        }
        private void OnDestroy()
        {
            lobbyScene_Ctrl.DesteryInstance();
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
        
      
    }
}

