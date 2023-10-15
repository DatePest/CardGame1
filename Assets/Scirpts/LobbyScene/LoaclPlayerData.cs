using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaclPlayerData : Singleton_T_Mono<LoaclPlayerData>
{
    public string PlayerName { get; private set; }
    private void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string s)
    {
        PlayerName = s;
    }
}
