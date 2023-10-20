using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public  class  Event_Ability_Addbuff : Event_AbilityBase
{
    public RoundTimeBuffSO buffSO;

    public int TimeExtension = 0;

    public void IEvent(SO_Unit Unit)//‘¢¬ŠQšdˆÊ ,Žó“žŠQšdˆÊ
    {
        var B = Unit.AddBuff(buffSO);
        B.TimeAdd(TimeExtension);
        //Debug.Log(Unit.UID+ "Sub_IEvent >> addbuff");
    }
}
public enum AddTarget : byte
{
    AtkUnit,
    HurtUnit
}
