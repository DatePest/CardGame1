using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public  class  Event_Ability_RemoveBuff : Event_AbilityBase
{
    public RoundTimeBuffSO buffSO;

    public void IEvent(SO_Unit Unit)//�������Q�d�� ,�󓞏��Q�d��
    {
        BuffBase B=null;
       foreach(var a in Unit.Buffs)
        {
            if(a.BuffID == buffSO.BuffID)
            {
                B = a;
                break;
            }
        }

        if (B != null)
        {
            Unit.RemoveBuff(B);
        }
    }
}
