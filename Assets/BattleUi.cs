using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUi : MonoBehaviour
{
    [SerializeField] ObjPool objPool;
    int Before=0;
    // Start is called before
    public void BattleShomScreen(ref Round_IState T )
    {
        //gameObject.SetActive(true);
        //Round_IState T = null;
        //CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);
        Round_Battle B = T as Round_Battle;
        B.BattleActions_Event += BattleList;
        B.BattleActions_ClearEvent += BattleListClear;
    }
    public void CurrnetAtk(int i)
    {
        if (Before != i)
        {
            var T = objPool.EnableList[Before].GetComponent<BattleActionPool>();
            T.BG_White_Color();
        }

        var P = objPool.EnableList[i].GetComponent<BattleActionPool>();
        P.BG_Red_Color();
        Before = i;
    }

    private void BattleListClear()
    {
        foreach(var a in objPool.EnableList)
        {
            var P = a.GetComponent<BattleActionPool>();
            P.Clear();
        }
        Before = 0;
        objPool.RemoveAll();
        //gameObject.SetActive(false);
    }

    private void BattleList(AttatkBase obj)
    {
        objPool.GetPool(out var T);
        var P =T.GetComponent<BattleActionPool>();
        P.SetAttatkAction(obj);
    }
}
