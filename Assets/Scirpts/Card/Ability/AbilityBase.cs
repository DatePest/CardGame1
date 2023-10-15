using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  AbilityBase :ScriptableObject
{
    public string AbilityId;
    //public bool IsBuffAdd=false;
    public abstract void AddUseAbility(SO_Unit unit);

    public abstract void RemoveAbility(SO_Unit unit);

    public AbilityBase DeepCopy()
    {
        var C = Instantiate(this);
        return C;
    }
    public abstract void Stack(bool IsAdd);
}

public enum AbilityTpye
{
    HP, Armor, Attack, Armor_Atk
}
public enum AbilityMultiplierTpye
{
    HP, Armor, Attack
}

public static class AbilityBaseAPI
{
    public static int GetAbility(SO_Unit unit , AbilityTpye ability )
    {
        switch (ability)
        {
            case AbilityTpye.HP:
                return unit.LastMaxHP;
            case AbilityTpye.Armor:
                return unit.LastArmor;
            case AbilityTpye.Attack:
                return unit.LastAtk;
            case AbilityTpye.Armor_Atk:
                return unit.ArmorAtk;
            default:
                throw new ArgumentNullException("AbilityTpye Is Null");
        }
    }
    public static void GetAbilitySetFunc(SO_Unit unit, int I , AbilityTpye ability)
    {
        switch (ability)
        {
            case AbilityTpye.HP:
                unit.SetModfirMaxHP(I);
                break;
            case AbilityTpye.Armor:
                unit.SetModfirArrmor(I);
                break;
            case AbilityTpye.Attack:
                unit.SetModfirAtk(I);
                break;
            case AbilityTpye.Armor_Atk:
                unit.SetArmorAtk(I);
                break;
            default:
                throw new ArgumentNullException("AbilityTpye Is Null");
        }
    }
    public static void GetAbilityMultiplierSetFunc(SO_Unit unit, float I, AbilityMultiplierTpye ability,bool b =false)
    {
        switch (ability)
        {
            case AbilityMultiplierTpye.HP:
                unit.SetModfirMultiplierMaxHP(I,b);
                break;
            case AbilityMultiplierTpye.Armor:
                unit.SetModfirMultiplierArrmor(I,b);
                break;
            case AbilityMultiplierTpye.Attack:
                unit.SetModfirMultiplierAtk(I,b);
                break;
            default:
                throw new ArgumentNullException("AbilityTpye Is Null");
        }
    }
}
