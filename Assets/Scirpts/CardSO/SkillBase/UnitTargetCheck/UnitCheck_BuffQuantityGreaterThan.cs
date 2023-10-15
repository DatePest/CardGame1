using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitCheck_BuffQuantityGreaterThan", menuName = "SO/UnitCheck/BuffQuantityGreaterThan")]
public class UnitCheck_BuffQuantityGreaterThan : UnitCheckBase
{
    [SerializeField] int CheckBuff;
    [SerializeField] CheckBuffType CheckBufftype;
    public override  bool UseCheck(Unit unit)
    {
        int i = 0;
        foreach(var a in unit.UnitData.Buffs)
        {
            if (GetBuffType(a.buffTpye))
            {
                i++;
                if(i >= CheckBuff)
                    return F.ReNox(true);
            }
        }

        
        return F.ReNox(false);
    }

    bool GetBuffType(BuffType B)
    {
        if (CheckBufftype == CheckBuffType.All)
            return true;
        if (CheckBufftype == CheckBuffType.IsBuff && B == BuffType.Buff)
            return true;
        if (CheckBufftype == CheckBuffType.IsDebuff && B == BuffType.DeBuff) 
            return true;

        return false;
    }

    public enum CheckBuffType : byte
    {
        IsBuff,IsDebuff,All
        
    }
}
