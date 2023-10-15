using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_TSchange_Toskill", menuName = "SO/AbilityBase/Event_TSchange_Toskill")]
public class Ability_Event_TSchange_Toskill : TSchange_Event
{
    [SerializeField] Event_Ability_UseSkill_NotTarge EventUseSkill;
    protected override void IEvent(int obj)
    {
        if(CurrentTpye != CardGameManager.Instance.Ts_Manager.TS_NowTpye)
        {
            EventUseSkill.IEvent(Myunit);
        }
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
