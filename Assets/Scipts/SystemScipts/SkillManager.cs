using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillManager : Singleton_T_Mono<SkillManager>
{
    //SkillSystem system { get { return SkillSystem.Instance; } }
    void Start()
    {
        
    }

    //public void UseSkill(string Type, AbilityNeedData data, bool IsWaitUserData)
    //{
    //    try
    //    {
    //        var action = SkillSystem.Instance.SkillData[Type];
    //        if (IsWaitUserData)
    //            StartCoroutine(AbilityDataWait(data, action));
    //        else
    //            action.Invoke(data);
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogException(ex);
    //        Debug.LogError($"{Type}  is null");
    //    }

    //}

    //private IEnumerator AbilityDataWait(AbilityNeedData data, Action<AbilityNeedData> A)
    //{
    //    while (data.AbilityToTarget == null)
    //    {

    //        var ToTarget = MouseManager.Instance.CurrnetSelect.GetComponent<SO_Unit>();
    //        if (ToTarget != null)
    //            data.AbilityToTarget = ToTarget;

    //        yield return null;
    //    }
    //    A.Invoke(data);
    //}
}
