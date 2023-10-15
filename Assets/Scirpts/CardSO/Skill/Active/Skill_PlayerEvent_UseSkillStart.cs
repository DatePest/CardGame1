using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerEvent_UseSkillStart", menuName = "SO/CardSkill/PlayerEvent/UseSkillStart")]
public sealed class Skill_PlayerEvent_UseSkillStart : Skill_NotTarget
{
    //[SerializeField] int  Times = 1;
    [SerializeField] UseSkillStartTpye eventTpye;


    public override async Task UseSkill(AbilityNeedData data)
    {
        UseSkillStartTpye[] Array = (UseSkillStartTpye[])Enum.GetValues(typeof(UseSkillStartTpye));

        foreach (var a in Array)
        {
            if (a == eventTpye)
            {
                data.CurrentUsePlayer.UseStartSkill += PlayerEvent_UseSkillStart.PlayerEventDictionary[a];
                break;
            }
                
        }
    }
}
public enum UseSkillStartTpye : byte
{
    SKill_IsTs_01,
    SKill_IsTs_02,
    SKill_IsEX_01,
}
public static class PlayerEvent_UseSkillStart
{
    public static Dictionary<UseSkillStartTpye, Action<AbilityNeedData, SO_SKillAbility>> PlayerEventDictionary = new();
    static PlayerEvent_UseSkillStart()
    {
        PlayerEventDictionary.Add(UseSkillStartTpye.SKill_IsTs_01, SKill_IsTs_01);
        PlayerEventDictionary.Add(UseSkillStartTpye.SKill_IsTs_02, SKill_IsTs_02);
        PlayerEventDictionary.Add(UseSkillStartTpye.SKill_IsEX_01, SKill_IsEX_01);
    }
     static void SKill_IsTs_01(AbilityNeedData data, SO_SKillAbility sObj_SKill)
    {
        if (sObj_SKill is Skill_TS)
        {
            var T = sObj_SKill as Skill_TS;
            if (T.TSSkillType == TS_Tpye.DEX)
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(+1);
            else if (T.TSSkillType == TS_Tpye.WEX)
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(-1);

            data.CurrentUsePlayer.UseStartSkill -= SKill_IsTs_01;
        }
        return;
    }
     static void SKill_IsTs_02(AbilityNeedData data, SO_SKillAbility sObj_SKill)
    {
        if (sObj_SKill is Skill_TS)
        {
            var T = sObj_SKill as Skill_TS;
            if (T.TSSkillType == TS_Tpye.DEX)
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(+2);
            else if (T.TSSkillType == TS_Tpye.WEX)
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(-2);

            data.CurrentUsePlayer.UseStartSkill -= SKill_IsTs_02;
        }
        return;
    }
    static void SKill_IsEX_01(AbilityNeedData data, SO_SKillAbility sObj_SKill)
    {
        if (sObj_SKill is Skill_TS)
        {
            var T = sObj_SKill as Skill_TS;
            if (T.TSSkillType == TS_Tpye.DEX)
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(+1);
            else if (T.TSSkillType == TS_Tpye.WEX)
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(-1);

            data.CurrentUsePlayer.UseStartSkill -= SKill_IsEX_01;
        }
        return;
    }

}
