using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_BeAddBuff_ToSkill", menuName = "SO/AbilityBase/BeAddBuff")]
public class Ability_Event_BeAddBuff_ToSkill : BeAddBuff_Event 
{
    [SerializeField] Buff_AnyTrun BeAddbuff;

    [SerializeField] Event_Ability_UseSkill_NeedTarge AddbuffBase;
    
    protected override void IEvent(SO_Unit arg1, BuffBase arg2)
    {
        if (BeAddbuff != null && BeAddbuff.Check(arg2)) return;
        AddbuffBase.IEvent(arg1);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
