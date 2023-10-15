using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class UnitCheckBase : ScriptableObject
{
    [SerializeField] protected ReTurnFox F;
    public abstract  bool UseCheck(Unit unit);
   
}


