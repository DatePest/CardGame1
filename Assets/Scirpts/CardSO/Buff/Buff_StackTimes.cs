using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Buff_StackTimes", menuName = "SO/Buff/Buff_StackTimes")]

public class Buff_StackTimes : RoundTimeBuffSO
{
    Buff_StackTimes()
    {
        IsCanStack = true;
    }
    
    public override void Stack_(bool IsAdd)
    {
        TimeAdd(1);
        //if (IsAdd)
        //{
        //    StackTimes++;
        //}
        //else
        //{
        //    StackTimes--;
        //}
    }
}
