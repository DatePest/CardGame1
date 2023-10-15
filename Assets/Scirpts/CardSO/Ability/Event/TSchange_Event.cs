using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TSchange_Event : AbilityBase
{
    protected TS_Tpye CurrentTpye;
    protected SO_Unit Myunit;
    public override void AddUseAbility(SO_Unit unit)
    {
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action += IEvent;
        CurrentTpye = CardGameManager.Instance.Ts_Manager.TS_NowTpye;
        Myunit = unit;
    }

    protected abstract void IEvent(int obj);

    public override void RemoveAbility(SO_Unit unit)
    {
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action -= IEvent;
        Myunit = null;
    }
}
