using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_T<T>  where T : class, new()
{
    private static T instance;
    private static readonly object lockObj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null) instance = new T();
                }
            }
            return instance;
        }
    }
    public void DesteryInstance()
    {
        instance = null;
    }
}