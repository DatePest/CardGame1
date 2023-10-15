using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class MainSingleTestScrpt : MonoBehaviour
{
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        GameManager.Instance.SceneManager.EnableNetSceneManager();
        NetworkManager.Singleton.SceneManager.LoadScene(Scene.PvPLobby.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    public void client()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void CardDeckCustom()
    {
        GameManager.Instance.SceneManager.LocalLoadSceneAsync(Scene.CardDeckCustom);
    }

    private void Update()
    {
        //Debug.Log(NetworkManager.Singleton.ConnectedClients.Count);
        //if (!NetworkManager.Singleton.IsHost) return;
        if (Input.GetKeyDown(KeyCode.T) 
            //&& NetworkManager.Singleton.ConnectedClients.Count >1
            )
        {
            //GameManager.Instance.SceneManager.EnableNetSceneManager();
            //NetworkManager.Singleton.SceneManager.LoadScene(Scene.PvPLobby.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
            GameManager.Instance.SceneManager.LocalLoadSceneAsync(Scene.PvPScene);
        }
    }
}
