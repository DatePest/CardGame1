using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceSystem 
{
    public static UnitHitTpye HitDice(Unit AtkUnit,Unit DmgUnit)
    {
        UnitHitTpye Hit = new();

        int Roll = Random.Range(1, 100);
        int Dodge =  DmgUnit.UnitData.LastDodgeRate;
        int MissRate = AtkUnit.UnitData.LastMissRate;
        

        if(Roll <= Dodge)
            Hit = UnitHitTpye.Dodge;
        else if(Roll <=MissRate)
            Hit = UnitHitTpye.Miss;
        else
            Hit = UnitHitTpye.Hit;
       



        return Hit;
    }
}
