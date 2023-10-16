using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TS_script : MonoBehaviour
{
    public RawImage[] Images;

    private void Awake()
    {
        Images = GetComponentsInChildren<RawImage>();
    }
    private void Start()
    {
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action += UpdataRawImage;
        UpdataRawImage(CardGameManager.Instance.Ts_Manager.TsTimes);
    }
    private void OnDisable()
    {
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action -= UpdataRawImage;
    }

    public void UpdataRawImage(int T)
    {
        foreach (var a in Images)
        {
            a.color = Color.white;
        }
        for (int i = 0; i < T; i++)
        {
            Images[i].color = Color.black;
        }
    }
}


