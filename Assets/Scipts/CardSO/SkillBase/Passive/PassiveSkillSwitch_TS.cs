using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillSwitch", menuName = "SO/CardSkill/PassiveSkillBase/PassiveSkillSwitch")]
public class PassiveSkillSwitch_TS : PassiveSkillSwitch
{
    [SerializeField] TS_Tpye TS_Tpye;
    //public override bool Sub_Switch()
    //{
    //    if (IsAddEvent == true)
    //    {
    //        CardGameManager.Instance.Ts_Manager.TS_UpData_Action -= AEvent;
    //        CardGameManager.Instance.Ts_Manager.TS_UpData_Action += AEvent;
           
    //    }
    //    else
    //    {
    //        CardGameManager.Instance.Ts_Manager.TS_UpData_Action += AEvent;
    //        IsAddEvent = true;
    //    }

    //    if (TS_Tpye == CardGameManager.Instance.Ts_Manager.TS_NowTpye)
    //        return (true);
    //    else
    //        return (false);
    //}

    public override bool CheckTs()
    {
        if (TS_Tpye == CardGameManager.Instance.Ts_Manager.TS_NowTpye)
            return true;
        else
            return false;
    }

}