using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hero005A_Event_BePurified_Addbuff", menuName = "SO/AbilityBase/Hero/Hero005A")]
public class Ability_Event_BePurified_Addbuff_Hero005A : BePurified_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;

    protected override void IEvent(SO_Unit arg1, int arg2)
    {
        for (int i = 0; i < arg2 ;i++)
        {
            AddbuffBase.IEvent(arg1);
        }
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
