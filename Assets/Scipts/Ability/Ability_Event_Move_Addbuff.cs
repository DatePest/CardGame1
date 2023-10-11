using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Event_Move_Addbuff", menuName = "SO/AbilityBase/Evene_Move_Addbuff")]
public class Ability_Event_Move_Addbuff: Move_Event
{
    [SerializeField] Event_Ability_Addbuff AddbuffBase;

    protected override void IEvent(SO_Unit obj)
    {
        AddbuffBase.IEvent(obj);
    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
