using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hero005B_Event_Ability_BeAddBuff", menuName = "SO/AbilityBase/Hero/Hero005B")]
public class Ability_Event_BeAddBuff_Hero005B : BeAddBuff_Event 
{
    [SerializeField] Event_Ability_UseSkill_NeedTarge AddbuffBase;
    
    protected override void IEvent(SO_Unit arg1, BuffBase arg2)
    {
        int i = 0;
        foreach (var a in arg1.Buffs)
        {
            if (a.buffTpye == BuffType.DeBuff)
            {
                i++;
                if (i >= 4)
                {
                    AddbuffBase.IEvent(arg1);
                    return;
                }

            }
        }
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }

}
