using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
namespace LobbyScene
{
    public class LobbyScene_Ctrl 
    {
        public static LobbyScene_Ctrl Instance { get; private set; }
        LobbySceneManager lobbySceneManager;
        bool Startlook = false;
        public void DesteryInstance()
        {
            Instance = null;
        }
        public LobbyScene_Ctrl(LobbySceneManager Manager)
        {
            lobbySceneManager = Manager;
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Debug.LogError("LobbyScene_Ctrl Instance Is NotNull");
        }

        #region Button
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
                //Debug.Log("GameStartButton");
                lobbySceneManager.lobbyState_.GetDeckData_ServerRpc();
            }
        }

        public void ReadyButton()
        {
            if (!lobbySceneManager.lobbyLoadDeck.DeckCheck) return;
            foreach (var a in lobbySceneManager.Playersolt)
            {
                if (a.OwnerID == NetworkManager.Singleton.LocalClientId)
                {
                    a.IsReadyUpdata();
                    return;
                }
            }
        }
        public void LobbyExitButton()
        {
            GameManager.Instance.steamManager.LobbyLeave();
            GameManager.Instance.Transport.Shutdown();
            NetworkManager.Singleton.Shutdown();
            GameManager.Instance.SceneManager.LocalLoadSceneAsync(Scene.TestMain);
        }

        public void CheckAllReady()
        {
            var T = lobbySceneManager.lobbyButton.GetButton(LobbyButton_._Start);
            foreach (var a in lobbySceneManager.Playersolt)
            {
                if (a.IsReady == false)
                {
                    T.gameObject.SetActive(false);
                    return;
                }

            }
            T.gameObject.SetActive(true);
        }
        public void HideLoadDeckButton(bool b)
        {
            if (b)
                lobbySceneManager.lobbyLoadDeck.LoadDeckButton.gameObject.SetActive(false);
            else
                lobbySceneManager.lobbyLoadDeck.LoadDeckButton.gameObject.SetActive(true);
        }

        public void PlayerReady(bool PlayerReady)
        {
            lobbySceneManager.lobbyState_.PlayerReady_ServerRpc(PlayerReady);
        }
        #endregion


        #region Use_lobbySceneManager
        public int GetPlayerNumbers()
        {
            return lobbySceneManager.Playersolt.Length;
        }
        public void LobbySet(int i, LobbyPlayerState State)
        {
            lobbySceneManager.Playersolt[i].SetOwner(State.ClientId, State.m_PlayerName);
            LobbySetReady(i, State.Ready);
        }
        public void LobbySetReady(int i, bool b)
        {
            lobbySceneManager.Playersolt[i].SetIsReady(b);
        }
        public void LobbyLeave(int i)
        {
            lobbySceneManager.Playersolt[i].LeaveOwnerSc();
        }

        public DeckData GetDeckData()
        {
            return lobbySceneManager.lobbyLoadDeck.GetDeck();
        }
        #endregion
    }



}

