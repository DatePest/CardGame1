using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class I_StatePattern
{
    
    abstract public void StateEnter();
    abstract public void StateUpdate();
    abstract public void StateExit();
    
}
