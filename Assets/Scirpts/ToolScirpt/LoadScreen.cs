using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] ObjPool ObjPool;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Show(string[] S, Action<string> LoadDeckData, string Extension)
    {
       foreach(var a in ObjPool.EnableList)
        {
            a.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
           
        }
        ObjPool.RemoveAll();
        foreach(var a in S)
        {
            string extension = Path.GetExtension(a);
            if (extension != Extension)
            {
                continue;
            }
            ObjPool.GetPool(out  GameObject G);
            G.GetComponentInChildren<Button>().onClick.AddListener(
                () =>
                {
                    LoadDeckData(a);
                });
            G.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(a);
        }
    }
}
