using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveSkill_TsEvent", menuName = "SO/CardSkill/PassiveSkillBase/Skill_TsEvent")]
public class Skill_TsEvent : PassiveRoundEventBase
{
    
    TS_Tpye? Current_TSTpye;

    public override void RemoveSkill()
    {
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action -= TsEvent;
        ResetIState.Round_Action -= Reset;
        Current_TSTpye = null;
        Data = null;
        ResetIState = null;
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        Data = data;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(RestTime, ref ResetIState);
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action += TsEvent;
        ResetIState.Round_Action += Reset;
        Current_TSTpye = CardGameManager.Instance.Ts_Manager.TS_NowTpye;
        return null;
    }


    private void TsEvent(int obj)
    {
        if (!CanRun()) return;
        if (Current_TSTpye.Value == CardGameManager.Instance.Ts_Manager.TS_NowTpye) return;
        Current_TSTpye = CardGameManager.Instance.Ts_Manager.TS_NowTpye;
        Current += Current;
        //Skillnotify();
        NotTarget();
        NeedTarget();
        NeedTS();
    }



}
