using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Netcode.Transports.Facepunch;
/// <summary>
/// 2023/7 ~ 2023~11
/// Developer   Programming DatePest // Art : •s–Å
/// </summary>
public class GameManager : Singleton_T_Mono<GameManager>
{
    [SerializeField]
    LoadSceneManager SceneManagerPrefab;
    [SerializeField]
    NetworkManager NetworkManager;
    [SerializeField]
    public DataBese   DataBase  { get; private set; }
    public FacepunchTransport Transport{ get; private set; }
    public SteamManager steamManager { get; private set; }
    public LoadSceneManager SceneManager { get; private set; }
    private void Awake()
    {

        if (GameManager.Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            steamManager = GetComponent<SteamManager>();
            NetworkManager = Instantiate(NetworkManager);
            Transport = NetworkManager.gameObject.GetComponent<FacepunchTransport>();
            SceneManager = Instantiate(SceneManagerPrefab, gameObject.transform);
            DataBase = GetComponent<DataBese>();
            Application.targetFrameRate = 60;
        }
        base.Awake();
        

    }


}
