using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_T_Mono<T> : MonoBehaviour where T : Singleton_T_Mono<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Get a second instance of this classÅF" + this.GetType());
            Destroy(gameObject);
        }
    }
}