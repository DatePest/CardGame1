using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class TS_Manager : MonoBehaviour
{
    //public NetworkVariable<int> NetTsTimes; //NetTsTimes=DEX

    /// TsTimes < 5 = WEX (îíêF)  TsTimes > 5 =DEX  (¸KêF)  TsTimes=DEX(¸KêF)  
    public int TsTimes { get; private set; }
    public event Action<int> TS_UpData_Action;
    public UIText StateUIText;
    public TS_Tpye TS_NowTpye
    {
        get
        {
            if (TsTimes < 5)
                return TS_NowTpye = TS_Tpye.WEX;
            else if (TsTimes > 5)
                return TS_NowTpye = TS_Tpye.DEX;
            else
                return TS_NowTpye = TS_Tpye.AVG;

        }
        private set { }
    }
    private void Start()
    {
        SetValueTsTimes(5);
    }
    public void SetValueTsTimes(int i)
    {
        TsTimes += i;
        if (TsTimes < 0) TsTimes = 0;
        else if (TsTimes > 10) TsTimes = 10;
        StateUIText.SetTMPro(TS_NowTpye.ToString());
        TS_UpData_Action?.Invoke(TsTimes);
        //Debug.Log("TS_NowTpye__" + TS_NowTpye);
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        SetValueTsTimes(-1);
    //    }
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        SetValueTsTimes(+1);
    //    }
    //}
}
public enum TS_Tpye
{
    WEX, DEX, AVG
}
