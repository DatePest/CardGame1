using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff_AnyTrun
{
    [SerializeField] List<BuffBase> buffs;
    public bool Check(BuffBase Buff) 
    {
        if (buffs.Count == 0) return true;

        bool B = false;
        foreach (var a in buffs)
        {
            B = Buff.BuffID == a.BuffID;
            if (B == true) break;
        }
        return B;
    }


}
