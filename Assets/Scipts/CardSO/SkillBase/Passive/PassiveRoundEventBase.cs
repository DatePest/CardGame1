using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class PassiveRoundEventBase : PassiveSkillBase
{

    [SerializeField] protected List<Skill_NotTarget> Skill_NotTargets;
    [SerializeField] protected List<Skill_NeedTargetBase> Skill_NeedTargets;
    [SerializeField] protected List<Skill_TS> Skill_TSs;

    [SerializeField] Check_CurrentST CheckTS;
    protected Round_IState ResetIState = null;
    [SerializeField] protected TargetRange Check_CurrentIs = TargetRange.Own;

    [SerializeField] protected Round_State_Enum RestTime;
    [SerializeField] protected int Round_UsageCount = 1;
    protected int Current = 0;



    protected bool CanRun()
    {
        if (CheckTS != null&& CheckTS.CheckCard(null) != true)  return false;

        //var b = IState.IsCurrentPlayer(Data.Value.CurrentUsePlayer);
        var b = CardGameManager.Instance.GameTurnSystem.CheckCurrentPlayer(Data.Value.CurrentUsePlayer);
        switch (Check_CurrentIs)
        {
            case (TargetRange.Enemy):
                if (b != false) return false;
                break;
            case (TargetRange.Own):
                if (b != true) return false;
                break;
        }


        if (Current < Round_UsageCount)
            return true;
        else
            return false;
    }
    protected void Reset()
    {
        Current = 0;
    }

    protected void NotTarget()
    {
        if (Skill_NotTargets.Count < 1) return;
        //Skillnotify();
        foreach (var a in Skill_NotTargets)
        {
            a.PassiveSkillRunSkill(Data.Value);
        }
    }
    protected void NeedTarget()
    {
        if (Skill_NeedTargets.Count < 1) return;
        //Skillnotify();
        foreach (var a in Skill_NeedTargets)
        {
            a.PassiveSkillRunSkill(Data.Value);
        }
    }
    protected void NeedTS()
    {
        if (Skill_TSs.Count < 1) return;
        //Skillnotify();
        foreach (var a in Skill_TSs)
        {
            if (a.Manual_Select == true && Data.Value.CurrentUsePlayer.OwnerClientId != NetworkManager.Singleton.LocalClientId) continue;
            a.PassiveSkillRunSkill(Data.Value);
        }
    }


}
