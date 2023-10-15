using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveSkill_AddBuffSub", menuName = "SO/CardSkill/PassiveSkillBase/AddBuffSub")]
public class Skill_PassiveAddBuffSub : PassiveRoundEventBase
{

    //[Header("·¢ìÆödà ûäåèämîF")]
    //[SerializeField] List<UnitCheck_Buff> Check;
    [SerializeField] Buff_AnyTrun BeAddbuff;
    public override void RemoveSkill()
    {
        //var b =Data ;
        Data.Value.UserTarget.UnitData.BeAddBuff -= _Event;
        ResetIState.Round_Action -= Reset;
        Data = null;
        ResetIState = null;
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        Data = data;
        if(ResetIState ==null) CardGameManager.Instance.GameTurnSystem.GetRoundiState(RestTime, ref ResetIState);
        Data.Value.UserTarget.UnitData.BeAddBuff += _Event;
        ResetIState.Round_Action += Reset;
        return null;
    }

   

    private  void _Event(SO_Unit U, BuffBase B)
    {
        if (!CanRun()) return;
        if (B.buffTpye == BuffType.Aura) return;
        if (!BeAddbuff.Check(B)) return;
       
        Current ++;
        NotTarget();
        NeedTarget();
        NeedTS();
    }

    

    
}

