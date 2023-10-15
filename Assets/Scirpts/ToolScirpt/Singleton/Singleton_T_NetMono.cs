using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class Singleton_T_NetMono<T> : NetworkBehaviour where T : Singleton_T_NetMono<T>
{ 
    //private static T instance;
    ////public static T Instance
    ////{
    ////    get { return instance; }
    ////}

    //protected virtual void Awake()
    //{

    //    if (instance == null)
    //    {
    //        instance = (T)this;
    //    }
    //    else
    //    {
    //        Debug.LogError("Get a second instance of this classÅF" + this.GetType());
    //    }
    //}
}