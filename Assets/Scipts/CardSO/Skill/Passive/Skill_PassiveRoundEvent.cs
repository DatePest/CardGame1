using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveSkill_RoundEvent", menuName = "SO/CardSkill/PassiveSkillBase/RoundEvent")]
public class Skill_PassiveRoundEvent : PassiveRoundEventBase
{

    [Header("·¢ìÆödà ûäåèämîF")]
    [SerializeField] List<UnitCheckBase> Check;
    [SerializeField] protected Round_State_Enum UseTimeTime;
    protected Round_IState UseTime = null;
    public override void RemoveSkill()
    {
        var b =Data ;
        ResetIState.Round_Action -= _Event;
        UseTime.Round_Action -= Reset;
        Data = null;
        ResetIState = null;
        UseTime = null;
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        Data = data;
        if(UseTime == null)
            CardGameManager.Instance.GameTurnSystem.GetRoundiState(UseTimeTime, ref UseTime);
        if (ResetIState == null)
            CardGameManager.Instance.GameTurnSystem.GetRoundiState(RestTime, ref ResetIState);
        UseTime.Round_Action += _Event;
        ResetIState.Round_Action += Reset;
        return null;
    }

   

    private  void _Event()
    {
        if (!CanRun()) return;
        //Debug.Log(GetType() + "Current" + Current);
        
        if (Check.Count != 0)
        {
            bool T;
            foreach (var a in Check)
            {
                T = a.UseCheck(Data.Value.UserTarget);
                if (T != true)
                    return;
            }
        }
        //Debug.Log(AbilityID);
        //Data.Value.UserTarget.UnitData.TakeDamger(10, null, UnitHitTpye.Hit, AttackDamgerTpye.Enforce);
        Current ++;
        NotTarget();
        NeedTarget();
        NeedTS();
    }

    

    
}

