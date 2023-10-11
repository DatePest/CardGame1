using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillSystem : Singleton_T<SkillSystem>
{
    SkillDB DB = new();
    public Dictionary<string, Action<AbilityNeedData>> SkillData { get; private set; } = new();
   

     public SkillSystem()
    {
        Initialization();
    }

    void Initialization()
    {
        foreach (Skill_UserTargetType value in Enum.GetValues(typeof(Skill_UserTargetType)))
        {
            var methodName = value.ToString();
            var methodInfo = typeof(SkillDB).GetMethod(methodName);
            SkillData.Add(methodName, (Action<AbilityNeedData>)Delegate.CreateDelegate(typeof(Action<AbilityNeedData>), DB, methodInfo));
        }
        foreach (Skill_UserTargetToTargetType value in Enum.GetValues(typeof(Skill_UserTargetToTargetType)))
        {
            var methodName = value.ToString();
            var methodInfo = typeof(SkillDB).GetMethod(methodName);
            SkillData.Add(methodName, (Action<AbilityNeedData>)Delegate.CreateDelegate(typeof(Action<AbilityNeedData>), DB, methodInfo));
        }
        foreach (Skill_CostType value in Enum.GetValues(typeof(Skill_CostType)))
        {
            var methodName = value.ToString();
            var methodInfo = typeof(SkillDB).GetMethod(methodName);
            SkillData.Add(methodName, (Action<AbilityNeedData>)Delegate.CreateDelegate(typeof(Action<AbilityNeedData>), DB, methodInfo));
        }
        //SkillData.Add(methodName, (Action<AbilityNeedData>)Delegate.CreateDelegate(typeof(Action<AbilityNeedData>), DB, methodInfo));
        //SkillData.Add(methodName, (Action<AbilityNeedData>)Action.CreateDelegate(typeof(Action<AbilityNeedData>), methodInfo));
        //SkillData.Add(nameof(SKill_UserTargetType.HealTatget), DB.HealTatget);
        //SkillData.Add(nameof(DB.DamgeTatget), DB.DamgeTatget);
        //SkillData.Add(nameof(DB.AttackTatget), DB.AttackTatget);
    }



   

}

public class SkillDB
{
    public void HealTatget(AbilityNeedData data )
    {
        //Debug.Log("HealTatget__" +data.UserTarget.UnitName + $"__HP==={ data.UserTarget.LastMaxHP}__+" + data.AbilityValue1);
        //data.UserTarget.TakeHeal(data.AbilityValue1);
        //Debug.Log("NowHP =" + data.UserTarget.UnitName + $"___{ data.UserTarget.cardNowHp})" );
    }
    public void DamgeTatget(AbilityNeedData data)
    {
        //data.UserTarget.TakeDamger(data.AbilityValue1, null);
        //Debug.Log("DamgeTatget"+ data.UserTarget.cardNowHp);
    }

    public void TargetAttackToTatget(AbilityNeedData data)
    {
        //data.AbilityToTarget.TakeDamger(data.UserTarget.LastAtk, data.UserTarget);
        //data.AbilityToTarget.TakeDamger(data.AbilityValue1, data.UserTarget) ;
    }

    public void Cost_A(AbilityNeedData data)
    {
    }
    public void Cost_B(AbilityNeedData data)
    {
    }
}

public enum Skill_UserTargetType
{
    HealTatget, DamgeTatget
}

public enum Skill_UserTargetToTargetType
{
    TargetAttackToTatget
}

public enum Skill_CostType
{
    Cost_A, Cost_B
}
