using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public abstract class Skill_NotTarget_All : Skill_NotTarget
{
    [SerializeField]
    protected TargetRange target;

}


