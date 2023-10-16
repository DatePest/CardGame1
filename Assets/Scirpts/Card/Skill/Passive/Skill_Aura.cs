using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveSkill_Aura", menuName = "SO/CardSkill/PassiveSkillBase/Skill_Aura")]
public class Skill_Aura : PassiveSkillBase
{
    [SerializeField] PassiveSkillSwitch PassivelSwitch;
    [SerializeField] TargetRange targetRange;
    [SerializeField] BuffBase buff;
    [SerializeField] bool AuraRange_Oneself =true;
    [SerializeField] List<UnitCheckBase> unitChecks;

    List<Unit> Units;
    Round_State_Enum updataTime = Round_State_Enum.Round_Start;
    Round_IState IState = null;

    bool IsOpen = false;
    public override void RemoveSkill()
    {
        AuraClose();
        if (PassivelSwitch != null)
        {
            CardGameManager.Instance.Ts_Manager.TS_UpData_Action -= AEvent;
        }
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        Data = data;
        if (IState == null)
        {
             CardGameManager.Instance.GameTurnSystem.GetRoundiState(updataTime, ref IState);
        }
        if (PassivelSwitch != null)
        {
            CardGameManager.Instance.Ts_Manager.TS_UpData_Action += AEvent;
        }

        UpdateAura();
        return null;
    }

    private void AEvent(int obj)
    {
        SwitchEvent(PassivelSwitch.CheckTs());
    }

    private void UpdateAura()
    {
        if (PassivelSwitch != null)
        {
             SwitchEvent(PassivelSwitch.CheckTs());
        }
        else
        {
            Aura();
        }
    }

    public void SwitchEvent(bool obj)
    {
        //Debug.Log("SwitchEvent" + obj);
        if (obj)
            Aura();
        else
            AuraClose();
    }

    void Aura()
    {
        //Debug.Log("Aura");
        var UList = GetRange();
        Units = UList;
        foreach (var a in UList)
        {
            var B =a.UnitData.AddBuff(buff);
            if (B != null)
            {
                B.IsCanDispel = false;
                if (B is RoundTimeBuffSO)
                {
                    RoundTimeBuffSO R = B as RoundTimeBuffSO;
                    R.SetIsNATimes(true);
                    R.buffTpye = BuffType.Aura;
                }
            }
        }
        if (IsOpen == false)
        {
            IState.Round_Action += UpdateAura;
            IsOpen = true;
        }
        
    }
    void AuraClose()
    {
        if (IsOpen == false) return;
        IState.Round_Action -= UpdateAura;
        foreach (var a in Units)
        {
            a.UnitData.RemoveBuff(buff);
        }
        //Data = null;
        //IState = null;
        IsOpen = false;
        Units = null;
    }

    List<Unit> GetRange()
    {
        var UList = CardGame_Ctrl_Net.Instance.GetAllSelectRange(Data.Value, targetRange);
        List<Unit> Last = new();
        foreach (var a in UList)
        {
            bool b = true;
            foreach (var B in unitChecks)
            {
                if(!B.UseCheck(a))
                {
                    b = false;
                    break;
                }
            }
            if (b) Last.Add(a);
        }

        if (AuraRange_Oneself == false)
        {
            if (Last.Contains(Data.Value.UserTarget))
            {
                Last.Remove(Data.Value.UserTarget);
            }
        }
           
        return Last;
    }
}