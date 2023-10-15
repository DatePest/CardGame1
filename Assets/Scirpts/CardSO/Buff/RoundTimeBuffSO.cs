using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "BuffData", menuName = "SO/Buff/RoundTimeBuffSO")]
public class RoundTimeBuffSO : BuffBase
{
    public bool IsNATimes = false;
    public int TimeRound =1;
    public Round_State_Enum EndToRound_;
    int NowTime;
    Round_IState IState = null;
    
    public override void Interrupt_abstract()
    {
        if (IState != null)
        {
            IState.Round_Action -= Time;
            IState = null;
        }
    }
    public void SetIsNATimes(bool b)
    {
        IsNATimes = b;
        if(b == true)
        {
            //IState.Round_Action -= Time;
            //IState = null;
            TimeRound = 2;
            NowTime = TimeRound;
        }
    }
    public override void OnBuffEnabled()
    {
        Action TargetAction = null ;
        NowTime = TimeRound;
         CardGameManager.Instance.GameTurnSystem.GetRoundiState(EndToRound_ , ref IState);;
        IState.Round_Action += Time;
    }
    public void Time()
    {
        if (IsNATimes == true) return;
        NowTime--;
        if (NowTime <= 0)
        {
            IState.Round_Action -= Time;
            Remove();
        }
    }

    public override void TimeAdd(int i)
    {
        TimeRound += i;
    }

    public override void Stack_(bool Isadd)
    {
        foreach (var a in Abilitys)
        {
            a.Stack(Isadd);
        }
        base.Stack_(Isadd);
        
    }

}
