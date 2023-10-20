using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using Unity.Netcode;
using Steamworks;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] GameObject Loading;
    [SerializeField] Image LoadingImage;
    [SerializeField] bool IsNet = false;
    private void Awake()
    {
        Loading.SetActive(false);
    }
    public void EnableNetSceneManager()
    {
        //Debug.Log("EnableNetSceneManager");

        if (IsNet != false || NetworkManager.Singleton.SceneManager == null)  return;
        IsNet = !IsNet;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += LoadComplete;
        NetworkManager.Singleton.SceneManager.OnLoad += Load;
    }

    private void OnEnable()
    {
        if (IsNet != false ||  NetworkManager.Singleton.SceneManager == null) return;
        IsNet = !IsNet;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += LoadComplete;
        NetworkManager.Singleton.SceneManager.OnLoad += Load;
    }
    private void OnDisable()
    {
        if (IsNet != true || NetworkManager.Singleton == null) return;
        IsNet = !IsNet;
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= LoadComplete;
        NetworkManager.Singleton.SceneManager.OnLoad -= Load;
    }

    private  void Load(ulong clientId, string sceneName, LoadSceneMode loadSceneMode, AsyncOperation asyncOperation)
    {
        LoadingImage.fillAmount = 0f;
        Loading.SetActive(true);
        LoadAsync(asyncOperation);
       // Debug.Log("Load");
    }

    private async void LoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        //Debug.Log("LoadComplete");
        await Task.Delay(600);
        Loading.SetActive(false);
        
    }
    public  void NetLoadSceneAsync(Scene scene)
    {
        //Debug.Log("NetLoadSceneAsync");
        NetworkManager.Singleton.SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }

     async void LoadAsync(AsyncOperation asyncOperation)
    {

        while(asyncOperation.progress < 0.9f)
        {
            await Task.Delay(200);
            LoadingImage.fillAmount = asyncOperation.progress;
        }
        LoadingImage.fillAmount = 1f;
    }
    public async void LocalLoadSceneAsync(Scene scene)
    {
        Debug.Log("LocalLoadSceneAsync");
        var Next = SceneManager.LoadSceneAsync(scene.ToString());
        Loading.SetActive(true);
        Next.allowSceneActivation = false;
        LoadingImage.fillAmount = 0f;

        while (Next.progress < 0.9f)
        {
            await Task.Delay(200);
            LoadingImage.fillAmount = Next.progress;
        }
        LoadingImage.fillAmount = 1f;
        Next.allowSceneActivation = true;
        await Task.Delay(200);
        Loading.SetActive(false);
    }


}
public enum  Scene 
{
    TestMain 
    , PvPLobby
     , PvPScene
        , CardDeckCustom
}
