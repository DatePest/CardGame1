using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveSkill_DisCardEvent", menuName = "SO/CardSkill/PassiveSkillBase/DisCardEvent")]
public class Skill_DisCardEvent : PassiveRoundEventBase
{
    //[Header("·¢ìÆûäåèämîF")]
    //[SerializeField] List<SO_CardCheckType> CardCheck;
    public override void RemoveSkill()
    {
        Data.Value.CurrentUsePlayer.SkillDisCardEvent -= Dis_Event;
        ResetIState.Round_Action -= Reset;
        Data = null;
        ResetIState = null;
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        Data = data;
        //if (Data != null)
        //{
        //    Debug.Log("NetworkObjectId" + Data.Value.CurrentUsePlayer.NetworkObjectId);
        //    Debug.Log("UnitID" + Data.Value.UserTarget.UnitID);
        //}
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_End_Event, ref ResetIState);
        Data.Value.CurrentUsePlayer.SkillDisCardEvent += Dis_Event;
        ResetIState.Round_Action += Reset;

        return null;
    }

    private void Dis_Event(int obj)
    {
        if (!CanRun()) return;
        //Data.Value.UserTarget.UnitData.TakeDamger(10, null, UnitHitTpye.Hit, AttackDamgerTpye.Enforce);
        Current++;
        NotTarget();
        NeedTarget();
        NeedTS();
        Debug.Log("Skill_DisCardEvent");
    }
}

