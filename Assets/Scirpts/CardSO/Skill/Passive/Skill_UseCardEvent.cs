using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveSkill_UseCardEvent", menuName = "SO/CardSkill/PassiveSkillBase/UseCardEvent")]
public class Skill_UseCardEvent : PassiveRoundEventBase
{
    [Header("·¢ìÆûäåèämîF")]
    [SerializeField] List<SO_CardCheckType> CardCheck;
    [SerializeField] bool IsOwnUseCard =true;
    PlayerOBJ TargetObj;
    public override void RemoveSkill()
    {
        //var b =(AbilityNeedData)Data ;
        if(TargetObj!=null) TargetObj.UseCard -= Unit_Event;

        ResetIState.Round_Action -= Reset;
        Data = null;
        ResetIState = null;
        TargetObj = null;
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        Data = data;
        //if (Data != null)
        //{
        //    Debug.Log("NetworkObjectId" + Data.Value.CurrentUsePlayer.NetworkObjectId);
        //    Debug.Log("UnitID" + Data.Value.UserTarget.UnitID);
        //}
        if (IsOwnUseCard)
        {
            TargetObj = Data.Value.CurrentUsePlayer;
        }
        else
        {
            TargetObj = CardGameManager.Instance.GetEnemyPlayer(Data.Value.CurrentUsePlayer.OwnerClientId);
        }
        TargetObj.UseCard += Unit_Event;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(RestTime, ref ResetIState);
        ResetIState.Round_Action += Reset;

        return null;
    }

   

    private  void Unit_Event(GameObject Card)
    {
        if (!CanRun()) return;
        if (CardCheck.Count != 0)
        {
            var T = false;
            foreach (var a in CardCheck)
            {
                T = a.CheckCard(Card);
                if (T != true)
                    return;
            }
        }
        //Data.Value.UserTarget.UnitData.TakeDamger(10, null, UnitHitTpye.Hit, AttackDamgerTpye.Enforce);
        Current ++;
        NotTarget();
        NeedTarget();
        NeedTS();
    }

    void UseCardSub(bool IsSub)
    {
        if (IsSub)
        {
            
            TargetObj.UseCard += Unit_Event;
        }
        else
        {
            TargetObj.UseCard -= Unit_Event;
        }
        
    }



}

