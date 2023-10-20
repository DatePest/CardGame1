using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ReTurnFox 
{
    [SerializeField] bool IsNot = false;
    public bool ReNox(bool B)
    {
        if (IsNot == false)
            return B;

        return !B;
    }
}
