using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public abstract class PassiveSkillBase : SO_SKillAbility
{
    protected AbilityNeedData? Data;

   
    public PassiveSkillBase()
    {
        skillType = SkillType.Passive;
    }
    protected  void Skillnotify()
    {
        CardGameManager.Instance.GameNotifyAction_Net.UnitSkillNotifyServerRpc(Data.Value.UserTarget.UnitID);

    }
    public sealed override void Card_ReadySkill(CardSolt cardSolt, float Time = 2, bool ifUseNotify = false)
    {
        return;
    }



}
public enum PassiveSkillType
{
    Stats ,Auras
}