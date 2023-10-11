using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class MainSingleTestScrpt : MonoBehaviour
{
    List<TestInt> testInts = new();
    TestInt A = new(99);
    TestInt B = new(33);
    TestInt C = new(229);

    private void Start()
    {
        ///Test 

        //testInts.Add(A);
        //testInts.Add(B);
        //testInts.Add(C);

        //foreach(var a in testInts)
        //{
        //    Debug.Log(a.I); 
        //}
        //testInts.Remove(B);
        //Debug.Log("remove");
        //foreach (var a in testInts)
        //{
        //    Debug.Log(a.I);
        //}





        //DateTime currentDateTime = DateTime.Now;
        //Debug.Log(Application.dataPath + "/Save/");
        //string currentDate = currentDateTime.ToString("yyyy-MM-dd-HH-mm");
        //string currentTime = currentDateTime.ToString("HH:mm:ss");

        //Debug.Log("Current Date: " + currentDate);
        //Debug.Log("Current Time: " + currentTime);
    }
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
        if (!NetworkManager.Singleton.IsHost) return;
        if (Input.GetKeyDown(KeyCode.T) && NetworkManager.Singleton.ConnectedClients.Count >1)
        {
            GameManager.Instance.SceneManager.EnableNetSceneManager();
            NetworkManager.Singleton.SceneManager.LoadScene(Scene.PvPLobby.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}

public class TestInt
{
    public int I;
    public TestInt(int i)
    {
        I = i;
    }
}
