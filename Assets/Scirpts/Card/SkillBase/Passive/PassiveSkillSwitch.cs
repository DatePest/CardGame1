using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveSkillSwitch : ScriptableObject
{
    //public event Action<bool> SwitchEvent;
    //[SerializeField]protected bool IsAddEvent = false;
    //public abstract bool Sub_Switch();
    public abstract bool CheckTs();
    //public void E_Switch(bool b)
    //{
    //    if(b)
    //        SwitchEvent?.Invoke(true);
    //    else
    //        SwitchEvent?.Invoke(false);
    //}

}

